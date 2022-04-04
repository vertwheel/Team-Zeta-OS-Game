using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{
    private GameObject timer; //Reference the timer object 
    private GameObject question; //Reference the questions 

    public List<Transform> answerList = new List<Transform>();
    public List<string> answerName = new List<string>();
    public List<bool> answerRight = new List<bool>();
    [SerializeField] GameObject Question1;
    [SerializeField] GameObject answers;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer"); //get the timer object
        question = GameObject.Find("Question1"); //get the Question 1 from scene
        timer.GetComponent<Timer2Script>().setTimer(30).begin(); //start the timer at 30 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Done()
    {
        foreach (bool b in answerRight)
        {
            if (b == false)
            {
                Debug.Log("Wrong");
                return;
            }
            if (b == true)
            {
                Debug.Log("right");
                Question1.SetActive(false);
                answers.SetActive(false);
                timer.GetComponent<Timer2Script>().setTimer(30).begin(); //Reset the timer 
                return;

            }
            Debug.Log("right");
            timer.GetComponent<Timer2Script>().setTimer(30).begin(); //Reset the timer 

        }
        return;
    }

   
}
