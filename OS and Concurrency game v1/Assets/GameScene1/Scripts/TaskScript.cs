using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    //Changing the object color to be blue when mouse move over
    private Color mouseOverColor = Color.blue;
    private Color originalColor = Color.yellow;
    public bool dragging = false;
    private float distance;


    [SerializeField] private int burst_time;
    [SerializeField] private int priority;

    public void Set_priority(int set_priority)

    {
        priority = set_priority;
    }

    public void Set_burst_time(int set_burst_time)
    {
        burst_time = set_burst_time;
    }

    public int Get_priority()

    {
        return priority;
    }

    public int Get_burst_time()
    {
        return burst_time;
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = mouseOverColor;
    }

//Will hide the tooltip
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;
        transform.Find("canvas").gameObject.SetActive(false);
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

    // When mouse enter the tooltip will show
    private void OnMouseOver()
    {
        transform.Find("canvas").gameObject.SetActive(true);
        transform.GetComponentInChildren<Text>().text = "bt:" + burst_time + "  p:" + priority;
            
    }
}
