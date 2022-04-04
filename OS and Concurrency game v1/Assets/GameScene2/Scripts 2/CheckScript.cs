using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// knows what questions is currently loaded
// calls the right check distance

public class CheckScript : MonoBehaviour
{
    [SerializeField] GameObject Question1;
    [SerializeField] GameObject Question2;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void CheckQuestion ()
    {
        if (Question1.activeSelf == true)
        {
            GameObject.Find("Question1").GetComponent<CheckDistance>().Done();
        }

        else if (Question2.activeSelf == true)
        {
            GameObject.Find("Question2").GetComponent<CheckDistance>().Done();

        }
    }
}
