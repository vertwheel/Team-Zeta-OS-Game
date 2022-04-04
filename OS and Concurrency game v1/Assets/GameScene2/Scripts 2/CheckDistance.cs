// This script is for gamescene 2
// Defines all the list and frame actions
// Process the answer condition check 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    private GameObject timer; //Reference the timer 
    private GameObject textManager; //references the text manager object
    [SerializeField] public TextClass introtext; //dialogue text for the intro

    public List<Transform> answerList = new List<Transform>();
    public List<string> answerName = new List<string>();
    public List<bool> answerRight = new List<bool>();
    [SerializeField] GameObject HideQuestion;          //Hide the question from the scene
    [SerializeField] GameObject NextShow;                  // Question transition
    [SerializeField] GameObject HideAnswer1;               // Question answers that wants to hide
    [SerializeField] GameObject HideAnswer2;
    [SerializeField] GameObject HideAnswer3;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer"); //get the timer object
        textManager = GameObject.Find("TextManager"); //get the text manager
        timer.GetComponent<Timer2Script>().setTimer(30).begin(); //start the timer at 30 seconds
        textManager.GetComponent<TextManager>().StartText(introtext);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // check answer is correct or not 
    public void Done()
    {
        foreach(bool b in answerRight)
        {
            if(b==false)
            {
                Debug.Log("Wrong");
                return;
                // nothing will happen clock will still counting down
            }

        }
        // if all answer correct actions below will happen 
        Debug.Log("right");
        HideQuestion.SetActive(false);
        NextShow.SetActive(true);
        HideAnswer1.SetActive(false);
        HideAnswer2.SetActive(false);
        HideAnswer3.SetActive(false);
        timer.GetComponent<Timer2Script>().setTimer(30).begin(); //Reset the timer to 30s
    }
}
