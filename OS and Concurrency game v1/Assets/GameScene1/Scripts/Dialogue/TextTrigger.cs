using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] public TextClass text;

    public void TriggerText() {

        FindObjectOfType<TextManager>().StartText(text);
    
    }
}
