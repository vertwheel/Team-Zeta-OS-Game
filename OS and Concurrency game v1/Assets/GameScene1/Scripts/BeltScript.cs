using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 This script animates texture applied on an object in a specified direction/offset
*/

public class BeltScript : MonoBehaviour
{
    // Scroll texture across object based on time 
    private float scrollSpeedX = 0f, scrollSpeedY = 0f;
    Renderer rend;

    // Get method for value of scroll speed on x axis
    public float GetScrollSpeedX()
    {
        return scrollSpeedX; 
    }
    
    // Method to change scroll speed on x axis from other classes
    public void SetScrollSpeedX(float newScrollSpeedX)
    {
        scrollSpeedX = newScrollSpeedX;
    }

    void Start()
    {
        rend = GetComponent<Renderer> ();
        rend.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
    
    

    //Will hide the tooltip
    //void OnMouseExit()
    //{
    //    transform.Find("canvas").gameObject.SetActive(false);
    //}
    // When mouse enter the tooltip will show
    //private void OnMouseOver()
    //{
      //  transform.Find("canvas").gameObject.SetActive(true);
      //  transform.GetComponentInChildren<Text>().text = "CPU box";

//    }
}
