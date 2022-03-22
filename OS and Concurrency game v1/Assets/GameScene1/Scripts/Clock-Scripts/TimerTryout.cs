using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTryout : MonoBehaviour
{
    [SerializeField] TimerScript timer1;

    private void Start()
    {
        timer1.setTimer(40).begin();
    }
}
