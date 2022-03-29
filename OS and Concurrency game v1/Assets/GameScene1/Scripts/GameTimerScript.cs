using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimerScript : MonoBehaviour
{

    [SerializeField] private TMP_Text uiText;

    private int timeCount;
    

    public int Time { get; private set; }

    //To reset the timer.
    private void Reset()
    {
        uiText.text = "00:00";
    }
    public GameTimerScript setTimer(int second)
    {
        Time = timeCount = second;
        return this;
    }

    public void begin()
    {
        StopAllCoroutines ();
        StartCoroutine (UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while(timeCount < 300)
        {
            UpdateUI(timeCount);
            timeCount++;
            Debug.Log(timeCount);
            yield return new WaitForSeconds(1f);
        }
        End();
    }

    private void UpdateUI(int second)
    {
        uiText.text = string.Format("{0:D2}:{1:D2}", second / 60, second % 60);
    }

    public void End()
    {
        Reset(); 

    }
}
