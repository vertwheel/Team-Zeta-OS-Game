using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 This script animates texture applied on an object in a specified direction/offset
*/

public class BeltScript : MonoBehaviour
{
    // Scroll texture based on time 

    public float scrollSpeedX = 0.5f, scrollSpeedY = 0.5f;
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
