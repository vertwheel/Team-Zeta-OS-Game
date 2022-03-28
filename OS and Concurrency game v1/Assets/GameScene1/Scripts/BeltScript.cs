using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 This script animates texture applied on an object in a specified direction/offset
*/

public class BeltScript : MonoBehaviour
{
    // Scroll texture across object based on time 
    private float scrollSpeedX = 0.15f, scrollSpeedY = 0f;
    Renderer rend;

    // Get method for value of scroll speed on x axis
    public float GetScrollSpeedX()
    {
        return scrollSpeedX; 
    }
    
    // Method to change scroll speed on x axis from other classes
    public float SetScrollSpeedX(float newScrollSpeedX)
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
    
    

}
