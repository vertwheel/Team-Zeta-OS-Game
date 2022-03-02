using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPointScript : MonoBehaviour
{
    private BoxCollider2D attachCollider;

    // Start is called before the first frame update
    void Start()
    {
        attachCollider = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("sas");
        
        if (other.name.Contains("Task")) //if the touching object name contains "task", any cloned task should have this property
        {
        //is it efficient to make this check every frame? no
        //will i change it if i dont need to? no lol
            if (other.GetComponent<TaskScript>().dragging == false)
            {
                Debug.Log("yoy");
                other.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
            }
        }
    }
}
