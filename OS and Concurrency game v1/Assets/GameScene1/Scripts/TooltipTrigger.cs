using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //trigger the show 
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show();
    }
    // trigger the hide 
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
