using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private Image uiFillImage;
    [SerializeField] private TMP_Text uiText;

    private int timeLeft;

    public int Time { get; private set; }

    private void reset()
    {
        uiText.text = "00:00";
        uiFillImage.fillAmount = 0f;
    }

    // This method will set the timer.
    public TimerScript setTimer(int second)
    {
        Time = timeLeft = second;
        return this;
    }

    public void begin()
    {
        StopAllCoroutines ();
        StartCoroutine (updateTimer());
    }

    private IEnumerator updateTimer()
    {
        while(timeLeft > 0)
        {
            updateUI(timeLeft);
            timeLeft--;
            yield return new WaitForSeconds(1f);
        }
        end();
    }

    private void updateUI(int second)
    {
        uiText.text = string.Format("{0:D2}:{1:D2}", second / 60, second % 60);
        uiFillImage.fillAmount = Mathf.InverseLerp (0, Time, second);
    }

    public void end()
    {
        reset(); 

    }
}
