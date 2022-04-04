// This script is for gamescene 2
// Defines all the list and frame actions
// Process the answer condition check 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{

    public List<Transform> answerList = new List<Transform>();
    public List<string> answerName = new List<string>();
    public List<bool> answerRight = new List<bool>();
    [SerializeField] GameObject HideQuestion;
    [SerializeField] GameObject NextShow;
    [SerializeField] GameObject HideAnswer1;
    [SerializeField] GameObject HideAnswer2;
    [SerializeField] GameObject HideAnswer3;
    // Start is called before the first frame update
    void Start()
    {
        
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
        HideAnswer1.SetActive(false);
        HideAnswer2.SetActive(false);
        HideAnswer3.SetActive(false);
    }
}
