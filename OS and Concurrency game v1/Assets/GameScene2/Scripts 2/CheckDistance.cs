using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{

    [SerializeField] public List<Transform> answerList = new List<Transform>();
    [SerializeField] public List<string> answerName = new List<string>();
    [SerializeField] public List<bool> answerRight = new List<bool>();
    [SerializeField] GameObject HideQuestion;
    [SerializeField] GameObject ShowNextQuestion;
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

    public void Done()
    {
        foreach(bool b in answerRight)
        {
            if(b==false)
            {
                Debug.Log("Wrong");
                return;
            }
            if (b == true)
            {
                Debug.Log("Right");
                HideQuestion.SetActive(false);
                ShowNextQuestion.SetActive(true);
                HideAnswer1.SetActive(false);
                HideAnswer2.SetActive(false);
                HideAnswer3.SetActive(false);
            }

        }
        Debug.Log("right");
        //answerList.Clear();
        //answerName.Clear();
    }
}
