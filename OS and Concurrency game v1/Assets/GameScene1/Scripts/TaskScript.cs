using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    //Changing the object color to be blue when mouse move over
    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.yellow;
    private bool dragging = false;
    private float distance;


    private int burst_time;
    private int priority;

    public void Set_priority(int set_priority)

    {
        priority = set_priority;
    }


    public void Set_burst_time(int set_burst_time)

    {
        burst_time = set_burst_time;
    }



    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = mouseOverColor;
    }
 
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }
 
    void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        dragging = true;
    }
 
    void OnMouseUp()
    {
        dragging = false;
    }



// Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;
        }
    }
}
