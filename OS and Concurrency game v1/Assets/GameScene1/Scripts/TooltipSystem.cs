using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    // set the referance 
    public Tooltip tooltip;

    public void Awake()
    {
        current = this;

    }
    // enable the tooltip to show 
    public static void Show()
    {
        current.tooltip.gameObject.SetActive(true);

    }


    // hide the tooltip
    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
