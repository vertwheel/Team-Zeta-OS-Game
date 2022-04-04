using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    


    //dont remove the [SerializeField] parts, program doesnt work without them for some reason
    [SerializeField] private GameObject taskPrefab; //defined in editor
    [SerializeField] private GameObject processQueue; //references the process queue object
    [SerializeField] private GameObject conveyorBelt; //references the conveyor belt object
    [SerializeField] private List<GameObject> pqAttachPoints; //references the process queue's attachment points
    [SerializeField] private List<GameObject> cbAttachPoints; //references the conveyor belt's attachment points
    private GameObject timer; //references the timer object
    private GameObject burstTimeClock; //references the burst timer clock object
    private GameObject quantumTimeClock; //references the time quantum clock object
    private GameObject textManager; //references the text manager object

    private int timeQuantum = 4; //the length of the time quantum for the round-robin game
    private bool quantumTimeClockStarted = false; //check whether the time quantum clock has started

    private GameObject Ntask;

    private GameObject tick;
    private GameObject canvas;
    private GameObject ftick;

    public int listTick = 0;
    
    
   
    [SerializeField] public TextClass introtext; //dialogue text for the intro
    [SerializeField] public TextClass FCFStext; //dialogue text for FCFS
    [SerializeField] public TextClass PQtext; //dialogue text for Priority Scheduling
    [SerializeField] public TextClass RRtext; //dialogue text for Round Robin

    [SerializeField] private List<GameObject> preList; //the predetermined spawn order of tasks
    [SerializeField] public List<GameObject> correctList; //the intended result order of tasks
    [SerializeField] private List<GameObject> resultList; //the recieved result order of tasks, compared to the correct list at the end
    enum GameTypes { Intro, FirstComeFirstServe, PriorityQueue, RoundRobin }; //stores the types of levels so far, to control spawn and scoring behaviour
    private static GameTypes leveltype = GameTypes.FirstComeFirstServe; //what type of level running currently
    private bool levelPlaying = false; //check whether level has reached time 0

    // Start is called before the first frame update    
    void Start()
    {
        processQueue = GameObject.Find("ProcessBox"); //get the process queue 
        foreach (Transform child in processQueue.transform) //get all the attachment points of the PQ, put in a list
        {
            pqAttachPoints.Add(child.gameObject);
        }

        conveyorBelt = GameObject.Find("TopBelt"); //get the top belt object from the conveyor belt prefab
        foreach (Transform child in conveyorBelt.transform) //get all the attachment points of the CB, put in a list
        {
            cbAttachPoints.Add(child.gameObject);
        }

        timer = GameObject.Find("Timer"); //get the timer object
        burstTimeClock = GameObject.Find("Burst Timer"); //get the burst time clock
        quantumTimeClock = GameObject.Find("Quantum Clock"); //get the time quantum clock
        textManager = GameObject.Find("TextManager"); //get the text manager

        quantumTimeClock.active = false; //deactivate the time quantum clock


        tick = GameObject.Find("Check");
        canvas = GameObject.Find("Canvas");
        canvas.GetComponent<hideandshow>().hide(tick);

        ftick = GameObject.Find("Fcheck");
        canvas.GetComponent<hideandshow>().hide(ftick);

        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        //keybinds for testing purposes
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    spawnTask(1,2);
        //}
        //if (Input.GetKeyDown(KeyCode.KeypadEnter))
        //{
        //    endLevel();
        //}

        if ((timer.GetComponent<TimerScript>().getTimeLeft() == 0) && levelPlaying)
        {
            endLevel();
            levelPlaying = false;
        }
    }

    //start the dialogue for the corrosponding level
    private void startDialogue()
    {
        switch (leveltype)
        {
            case GameTypes.Intro:
                textManager.GetComponent<TextManager>().StartText(introtext);
                break;
            case GameTypes.FirstComeFirstServe:
                textManager.GetComponent<TextManager>().StartText(FCFStext);
                break;
            case GameTypes.PriorityQueue:
                textManager.GetComponent<TextManager>().StartText(PQtext);
                break;
            case GameTypes.RoundRobin:
                textManager.GetComponent<TextManager>().StartText(RRtext);
                quantumTimeClock.active = true;
                break;
        }
    }

    //prepare the level and start the gameplay behaviour for the corrosponding level
    public void startLevel()
    {
        conveyorBelt.GetComponent<AudioSource>().Play(); //start belt sound
        GameObject.Find("TopBelt").GetComponent<AudioSource>().volume = 0.01f; 

        burstTimeClock.GetComponent<GameTimerScript>().setTimer(0).begin(); //start the burst time clock

        conveyorBelt.GetComponent<BeltScript>().SetScrollSpeedX(0.15f); //start the belt animation
        GameObject.Find("BottomBelt").GetComponent<BeltScript>().SetScrollSpeedX(0.15f);

        switch (leveltype)
        {
            case GameTypes.Intro:
                endLevel();
                break;

            case GameTypes.FirstComeFirstServe:
                timer.GetComponent<TimerScript>().setTimer(40).begin(); //start the timer at 40 seconds
                break;

            case GameTypes.PriorityQueue:
                timer.GetComponent<TimerScript>().setTimer(60).begin(); //start the timer at 60 seconds

                for (int i = 0; i < 8; i++) //prepare a set of random tasks, but keep them deactivated
                {
                    GameObject newtask = spawnTask(Random.Range(1, 5), Random.Range(1, 10), false);
                    preList.Add(newtask);
                    newtask.active = false;
                }
                
                //correctList = preList.OrderBy<GameObject, int>(w => w.GetComponent<TaskScript>().Get_priority()).ToList();

                foreach (GameObject task in preList) //spawn all of the tasks
                {
                    spawnTask(task, true);
                }
                break;

            case GameTypes.RoundRobin:
                timer.GetComponent<TimerScript>().setTimer(60).begin(); //start the timer at 40 seconds
                quantumTimeClock.active = true;
                break;
        }
        levelPlaying = true;
    }

    //end the corrosponding level
    private void endLevel()
    {
        GameObject canvas = GameObject.Find("AfterGameCanvas");
        switch (leveltype)
        {
            case GameTypes.Intro:
                leveltype = GameTypes.FirstComeFirstServe;
                SceneManager.LoadScene("GameScene1");
                break;
            case GameTypes.FirstComeFirstServe:
                if (compareLists())
                { 
                    GameObject panel = canvas.transform.Find("SuccessPanel").gameObject;
                    panel.SetActive(true);
                }
                else
                {
                    GameObject panel = canvas.transform.Find("FailedPanel").gameObject;
                    panel.SetActive(true);
                }
                break;
            case GameTypes.RoundRobin:
                if (compareLists())
                {
                    GameObject panel = canvas.transform.Find("SuccessPanel").gameObject;
                    panel.SetActive(true);
                } 
                else
                {
                    GameObject panel = canvas.transform.Find("FailedPanel").gameObject;
                    panel.SetActive(true);
                }
                    
                break;
            case GameTypes.PriorityQueue:
                if (compareLists())
                { 
                    GameObject panel = canvas.transform.Find("SuccessPanel").gameObject;
                    panel.SetActive(true);
                }
                else
                {
                    GameObject panel = canvas.transform.Find("FailedPanel").gameObject;
                    panel.SetActive(true);
                }
                break;
        }
    }

    //load the next level
    public void loadNext()
    {
        switch (leveltype)
        {
            case GameTypes.FirstComeFirstServe:
                leveltype = GameTypes.PriorityQueue;
                SceneManager.LoadScene("GameScene1");
                break;
            case GameTypes.PriorityQueue:
                leveltype = GameTypes.RoundRobin;
                SceneManager.LoadScene("GameScene1");
                break;
        }
    }

    private void tickCheck()
    {
        canvas.GetComponent<hideandshow>().show(tick);
        canvas.GetComponent<hideandshow>().hide(ftick);

    }

    private void tickFcheck()
    {
        canvas.GetComponent<hideandshow>().hide(tick);
        canvas.GetComponent<hideandshow>().show(ftick);
    }

    //Called whenever the clock ticks
    public void onTick()
    {
        updateBelt();


        switch (leveltype)
        {
            case GameTypes.FirstComeFirstServe:
                
                if (timer.GetComponent<TimerScript>().getTimeLeft() > 20) //dont spawn anything in the last 20 sec to allow for remaining tasks to be consumed
                {
                    //for having a random delay between spawns
                    //pick a number between 1 and any number, if its 1 then proceed else dont do anything
                    //set to 1 for 100% chance to spawn every tick, 2 for 50% chance to spawn every tick, etc
                    int diceroll = Random.Range(1, 3);
                    if (diceroll == 1)
                    {
                        GameObject newtask = spawnTask(Random.Range(2, 5), 0, true);
                        if (newtask != null)
                        {
                            correctList.Add(newtask);
 
                            
                        }
                    }
                }
                break;
            case GameTypes.PriorityQueue:
                
                break;
            case GameTypes.RoundRobin:
                
 
                if (timer.GetComponent<TimerScript>().getTimeLeft() > 30) //dont spawn anything in the last 30 sec to allow for remaining tasks to be consumed
                {
                    if (timer.GetComponent<TimerScript>().getTimeLeft() % 10 == 9)
                    {
                        for (int i = 0; i < 2; i++)
                        {

                            GameObject newtask = spawnTask(Random.Range(4, 8), 0, true);
                            if (newtask != null)
                                correctList.Add(newtask);
                        }       
                    }
                }
                break;
       
        
        
        }
        


    }

    public void quantumTick()
    {
        GameObject firstTask = correctList[resultList.Count];
        correctList.RemoveAt(resultList.Count);
        correctList.Insert(correctList.Count, firstTask);
    }

    //move and update the items on the belt
    private void updateBelt()
    {

        //move every task on the belt along the belt
        for (int i = cbAttachPoints.Count - 2; i >= 0; i--)
        {
            GameObject ap1 = cbAttachPoints[i];
            GameObject ap2 = cbAttachPoints[i + 1];
            if ((ap1.GetComponent<AttachPointScript>().attachedTask != null) && (ap2.GetComponent<AttachPointScript>().attachedTask == null))
            {
                ap2.GetComponent<AttachPointScript>().addTask(ap1.GetComponent<AttachPointScript>().attachedTask);
                ap1.GetComponent<AttachPointScript>().removeTask();
            }
        }

        GameObject apLast = cbAttachPoints[cbAttachPoints.Count - 1];
        //When there is a task at the end of the belt
        if (apLast.GetComponent<AttachPointScript>().attachedTask != null)
        {
            if ((leveltype == GameTypes.RoundRobin) && !quantumTimeClockStarted)
            {
                quantumTimeClockStarted = true;
                quantumTimeClock.GetComponent<QuantumTimerScript>().setTimer(timeQuantum).begin();
            }


            GameObject taskLast = apLast.GetComponent<AttachPointScript>().attachedTask;
            if (taskLast.GetComponent<TaskScript>().Get_burst_time() > 1)
            {
                taskLast.GetComponent<TaskScript>().Set_burst_time(taskLast.GetComponent<TaskScript>().Get_burst_time() - 1);
                taskLast.transform.SetPositionAndRotation(taskLast.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), taskLast.transform.rotation);
            }
            else
            {
                resultList.Add(taskLast);

                if ((leveltype == GameTypes.FirstComeFirstServe) || (leveltype == GameTypes.RoundRobin))
                {
                    if (fcfsCheck())
                    {
                        tickCheck();
                    }
                    else
                    {
                        tickFcheck();
                    }
                }
                else
                {
                    if (!compareLists())
                    {


                        tickFcheck();
                        //listTick++;

                    }
                    else
                    {
                        tickCheck();
                        //listTick++;

                    }
                }

                apLast.GetComponent<AttachPointScript>().removeTask();
                taskLast.active = false; //instead of destroying, deactiveate task
            }


        }
    }
    private bool fcfsCheck()
    {
        for (int i = 0; i < resultList.Count; i++)
        {
            if (resultList[i] != correctList[i])
                return false;
        }
        return true;

    }

    private bool compareLists()
    {
        switch (leveltype)
        {
            case GameTypes.FirstComeFirstServe:
                if (correctList.Count != resultList.Count)
                    return false;
                for (int i = 0; i < correctList.Count; i++)
                {
                    if (correctList[i] != resultList[i])
                        return false;
                }
                return true;
            case GameTypes.PriorityQueue:
                int lastP = resultList[0].GetComponent<TaskScript>().Get_priority();
                for (int i = 1; i < resultList.Count; i++)
                {
                    if (resultList[i].GetComponent<TaskScript>().Get_priority() < lastP)
                        return false;
                    else
                        lastP = resultList[i].GetComponent<TaskScript>().Get_priority();
                }
                return true;
            case GameTypes.RoundRobin:
                if (correctList.Count != resultList.Count)
                    return false;
                for (int i = 0; i < correctList.Count; i++)
                {
                    if (correctList[i] != resultList[i])
                        return false;
                }
                return true;
            default:
                return false;
        }
    }

    //add a task in the next valid position in the process queue
    GameObject spawnTask(int burstTime, int priority, bool addTask)
    {
        foreach (GameObject ap in pqAttachPoints)
        {
            if (ap.GetComponent<AttachPointScript>().attachedTask == null)
            {
                GameObject newTask = Instantiate(taskPrefab, ap.transform.position, Quaternion.identity);
                newTask.GetComponent<TaskScript>().Set_burst_time(burstTime);
                newTask.GetComponent<TaskScript>().Set_priority(priority);
                //newTask.GetComponent<TaskScript>().Set_ID(Random.Range(1, 1000));
                if (addTask)
                {
                    ap.GetComponent<AttachPointScript>().addTask(newTask);
                }
                return newTask;
            }
        }
        return null;
    }

    GameObject spawnTask(GameObject task, bool addTask) //assuming the object is inactive
    {
        foreach (GameObject ap in pqAttachPoints)
        {
            if (ap.GetComponent<AttachPointScript>().attachedTask == null)
            {
                //GameObject newTask = Instantiate(taskPrefab, ap.transform.position, Quaternion.identity);
                task.active = true;
                if (addTask)
                {
                    ap.GetComponent<AttachPointScript>().addTask(task);
                }
                
                return task;
            }
        }
        return null;
    }
}
