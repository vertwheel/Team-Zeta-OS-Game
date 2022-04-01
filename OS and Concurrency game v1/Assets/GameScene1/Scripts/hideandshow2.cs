using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideandshow2 : MonoBehaviour

{
    // Start is called before the first frame update
    public void show(GameObject obj)
    {
        obj.SetActive(true);
    }

    // Update is called once per frame
    public void hide(GameObject obj)
    {
        obj.SetActive(false);
    }
}
