using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : MonoBehaviour
{

    public List<Transform> answerList = new List<Transform>();
    public List<string> answerName = new List<string>();
    public List<bool> answerRight = new List<bool>();
    [SerializeField] GameObject Question1;
    [SerializeField] GameObject answers;
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
                Debug.Log("right");
                Question1.SetActive(false);
                answers.SetActive(false);

            }

        }
        Debug.Log("right");
    }
}
