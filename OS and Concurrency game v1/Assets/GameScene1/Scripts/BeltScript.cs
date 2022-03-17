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
