using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab; //defined in editor
    private GameObject processQueue;
    
    // Start is called before the first frame update
    void Start()
    {
        processQueue = GameObject.Find("ProcessBox");
        Instantiate(taskPrefab, new Vector3(1.0F, 0, 0) /*+ processQueue.transform.position*/, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
