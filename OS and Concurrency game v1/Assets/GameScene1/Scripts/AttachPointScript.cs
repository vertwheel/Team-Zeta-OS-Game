using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPointScript : MonoBehaviour
{
    private BoxCollider2D attachCollider; //references the collider component
    public GameObject attachedTask = null; //stores the attached task

    // Start is called before the first frame update
    void Start()
    {
        attachCollider = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
    }

    // Update is called once per frame
    void Update()
    {
        //whenever the task is not being dragged, set it's pos to the attach point over time
        if (attachedTask != null)
        {
            if (attachedTask.GetComponent<TaskScript>().dragging == false)
            {
                attachedTask.transform.position = Vector3.Lerp(attachedTask.transform.position, gameObject.transform.position,0.5f);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        addTask(collision.transform.gameObject);
    }

    public void addTask(GameObject task)
    {
        if (task.name.Contains("Task")) //if the object name contains "task", any cloned task should have this property
        {
            //i had to change the check :((
            if (attachedTask == null)
            {
                attachedTask = task;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject == attachedTask)
        {
            removeTask();
        }
    }

    public void removeTask()
    {
        if (attachedTask != null)
        {
            attachedTask = null;
        }
    }
}
