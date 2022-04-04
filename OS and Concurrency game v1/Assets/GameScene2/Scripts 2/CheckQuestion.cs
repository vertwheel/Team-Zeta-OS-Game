// For gamescene2
// Checks which question is active on the scene right now
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckQuestion : MonoBehaviour
{
    [SerializeField] GameObject Question1;
    [SerializeField] GameObject Question2;
    [SerializeField] GameObject Question3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Checks which question is active on the scene right now
    // Call Done methods in the CheckDistance
    // Used for the button
    public void CheckQuestions()
    {
        if (Question1.activeSelf == true)
        {
            GameObject.Find("Question1").GetComponent<CheckDistance>().Done();
        }
        else if (Question2.activeSelf == true)
        {
            GameObject.Find("Question2").GetComponent<CheckDistance>().Done();
        }
        else if (Question3.activeSelf == true)
        {
            GameObject.Find("Question3").GetComponent<CheckDistance>().Done();
        }
    }
}
