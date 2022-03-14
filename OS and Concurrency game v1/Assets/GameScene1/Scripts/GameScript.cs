using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab; //defined in editor
    [SerializeField] private GameObject processQueue; //references the process queue object
    [SerializeField] private List<GameObject> pqAttachPoints; //references the process queue's attachment points

    // Start is called before the first frame update
    void Start()
    {
        processQueue = GameObject.Find("ProcessBox"); //get the process queue 
        foreach (Transform child in processQueue.transform) //get all the attachment points of the PQ, put in a list
        {
            pqAttachPoints.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //keybind for testing purposes
        if (Input.GetKeyDown(KeyCode.UpArrow))
        { 
            spawnTask();
            spawnTask();
            spawnTask();
            spawnTask();
            spawnTask();
            spawnTask();

        }
    }

    //add a task in the next valid position in the process queue
    void spawnTask()
    {
        foreach (GameObject ap in pqAttachPoints)
        {
            if (ap.GetComponent<AttachPointScript>().attachedTask == null)
            {
                ap.GetComponent<AttachPointScript>().addTask(Instantiate(taskPrefab, ap.transform.position, Quaternion.identity));
                return; //this probably isnt good code practice idk

            }
        }
    }
}
