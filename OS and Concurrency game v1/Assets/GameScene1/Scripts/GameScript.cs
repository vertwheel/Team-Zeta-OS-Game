using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
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

    [SerializeField] private List<GameObject> preList; //the predetermined spawn order of tasks
    [SerializeField] private List<GameObject> correctList; //the intended result order of tasks
    [SerializeField] private List<GameObject> resultList; //the recieved result order of tasks, compared to the correct list at the end
    enum GameTypes { FirstComeFirstServe, PriorityQueue, RoundRobin }; //stores the types of levels so far, to control spawn and scoring behaviour
    private GameTypes leveltype = GameTypes.PriorityQueue; //what type of level running currently
    private bool levelEnd = false; //check whether level has reached time 0

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

        conveyorBelt.GetComponent<AudioSource>().Play();

        burstTimeClock.GetComponent<GameTimerScript>().setTimer(0).begin();
        startLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //keybinds for testing purposes
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    spawnTask(1,2);
        //}
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log(compareLists());
        }

        if ((timer.GetComponent<TimerScript>().getTimeLeft() == 0) && !levelEnd)
        {
            endLevel();
            levelEnd = true;
        }
    }


    private void startLevel()
    {
        switch (leveltype)
        {
            case GameTypes.FirstComeFirstServe:
                timer.GetComponent<TimerScript>().setTimer(40).begin(); //start the timer at 40 seconds
                break;

            case GameTypes.PriorityQueue:
                timer.GetComponent<TimerScript>().setTimer(60).begin(); //start the timer at 40 seconds
                for (int i = 0; i < 8; i++)
                {
                    GameObject newtask = spawnTask(Random.Range(1, 5), Random.Range(1, 10), false);
                    preList.Add(newtask);
                    newtask.active = false;
                    //Debug.Log(newtask.GetComponent<TaskScript>().Get_priority());
                }
                
                //correctList = preList.OrderBy<GameObject, int>(w => w.GetComponent<TaskScript>().Get_priority()).ToList();

                foreach (GameObject task in preList)
                {
                    spawnTask(task, true);
                }
                break;

            case GameTypes.RoundRobin:
                timer.GetComponent<TimerScript>().setTimer(40).begin(); //start the timer at 40 seconds
                for (int i = 0; i < 8; i++)
                {
                    GameObject newtask = spawnTask(i, Random.Range(1, 3), false);
                    preList.Add(newtask);
                    newtask.active = false;
                    //Debug.Log(newtask.GetComponent<TaskScript>().Get_priority());
                }

                correctList = preList.OrderBy<GameObject, int>(w => w.GetComponent<TaskScript>().Get_burst_time()).ToList();

                foreach (GameObject task in preList)
                {
                    spawnTask(task, true);
                }
                break;
        }
    }

    private void endLevel()
    {
        switch (leveltype)
        {
            case GameTypes.FirstComeFirstServe:
                if (compareLists())
                {
                    Debug.Log("truu");
                }
                else
                {
                    Debug.Log("falsee");
                }
                break;
            case GameTypes.RoundRobin:
                break;
            case GameTypes.PriorityQueue:
                if (compareLists())
                {
                    Debug.Log("truu");
                }
                else
                {
                    Debug.Log("falsee");
                }
                break;
        }
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
                        GameObject newtask = spawnTask(Random.Range(1, 4), 0, true);
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
                break;
            
        }

        
    }

    //move and update the items on the belt
    private void updateBelt()
    {
        GameObject apLast = cbAttachPoints[cbAttachPoints.Count - 1];
        //When there is a task at the end of the belt
        if (apLast.GetComponent<AttachPointScript>().attachedTask != null)
        {
            GameObject taskLast = apLast.GetComponent<AttachPointScript>().attachedTask;
            if (taskLast.GetComponent<TaskScript>().Get_burst_time() > 1)
            {
                taskLast.GetComponent<TaskScript>().Set_burst_time(taskLast.GetComponent<TaskScript>().Get_burst_time() - 1);
                taskLast.transform.SetPositionAndRotation(taskLast.transform.position + new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)), taskLast.transform.rotation);
            }
            else
            {
                resultList.Add(taskLast);
                taskLast.active = false ; //instead of destroying, deactiveate task
            }
        }

        //move every task on the belt along the belt
        for (int i = cbAttachPoints.Count - 2; i >= 0 ; i--)
        {
            GameObject ap1 = cbAttachPoints[i];
            GameObject ap2 = cbAttachPoints[i+1];
            if ((ap1.GetComponent<AttachPointScript>().attachedTask != null) && (ap2.GetComponent<AttachPointScript>().attachedTask == null))
            {
                ap2.GetComponent<AttachPointScript>().addTask(ap1.GetComponent<AttachPointScript>().attachedTask);
                ap1.GetComponent<AttachPointScript>().removeTask();
            }
        }
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
                    //if (correctList[i].GetComponent<TaskScript>().Get_ID() != resultList[i].GetComponent<TaskScript>().Get_ID())
                    //    return false;
                    if (correctList[i] != resultList[i])
                        return false;
                }
                return true;
            case GameTypes.PriorityQueue:
                //if (correctList.Count != resultList.Count)
                //    return false;
                //for (int i = 0; i < correctList.Count; i++)
                //{
                //    //if (correctList[i].GetComponent<TaskScript>().Get_ID() != resultList[i].GetComponent<TaskScript>().Get_ID())
                //    //    return false;
                //    if (correctList[i] != resultList[i])
                //        return false;
                //}

                int lastP = resultList[0].GetComponent<TaskScript>().Get_priority();
                for (int i = 1; i < resultList.Count; i++)
                {
                    if (resultList[i].GetComponent<TaskScript>().Get_priority() < lastP)
                    {
                        return false;
                    } else
                    {
                        lastP = resultList[i].GetComponent<TaskScript>().Get_priority();
                    }
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
