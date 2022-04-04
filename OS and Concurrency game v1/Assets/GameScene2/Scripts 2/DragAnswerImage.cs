//This script is for gamescene2
// Do the draging and dropping for the answers and the question area

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAnswerImage : MonoBehaviour
{

    public CheckDistance cd;
    bool isDraging = false;
    float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        isDraging = true;
        
    }

    private void OnMouseUp()
    {
        isDraging = false;

        for(int i = 0; i < cd.answerList.Count; i++)
        {
            if(Vector3.Distance(cd.answerList[i].position, transform.position)<0.5f)
            {
                transform.position = cd.answerList[i].position;
                if(cd.answerName[i] == transform.name)
                {
                    cd.answerRight[i] = true;
                }
                else
                {
                    cd.answerRight[i] = false;
                }
            }
            
            //Debug.Log(Vector3.Distance(cd.answerList[i].position, transform.position));
        }


    }


    private void OnMouseOver()
    {
        if(isDraging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = new Vector3(rayPoint.x,rayPoint.y,0);

        }
       
       
    }
}
