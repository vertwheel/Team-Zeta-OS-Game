using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class CPUScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Will hide the tooltip
    void OnMouseExit() { 
        transform.Find("canvas").gameObject.SetActive(false);
    }
    // When mouse enter the tooltip will show
    private void OnMouseOver()
    {
        transform.Find("canvas").gameObject.SetActive(true);
        transform.GetComponentInChildren<Text>().text = "CPU box";

    }
}
