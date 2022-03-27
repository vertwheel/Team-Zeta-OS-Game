using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideandshow : MonoBehaviour
{
    // Start is called before the first frame update
    public void showIt(GameObject obj)
    {
        obj.SetActive(true);
    }

    // Update is called once per frame
    public void hideIt(GameObject obj)
    {
        obj.SetActive(false);
    }
}
