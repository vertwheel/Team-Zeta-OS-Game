using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTryout : MonoBehaviour
{
    [SerializeField] private GameTimerScript timer1;

    private void Start()
    {
        timer1.setTimer(0).begin();
    }
}
