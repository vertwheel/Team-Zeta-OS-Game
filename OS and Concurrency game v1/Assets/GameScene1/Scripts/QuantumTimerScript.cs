using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuantumTimerScript : MonoBehaviour
{
    [SerializeField] private Image uiFillImage;
    //[SerializeField] private TMP_Text uiText;

    private bool paused = false; //track whether the clock is paused
    private bool skipFirst = true;
    private int timeLeft;
    public Gradient gradient;   

    public int Time { get; private set; }

    private void reset()
    {
        //uiText.text = "00:00";
        //uiFillImage.fillAmount = 0f;

        //uiFillImage.color = gradient.Evaluate(1f);
        setTimer(Time);
        begin();
    }

    // This method will set the timer.
    public QuantumTimerScript setTimer(int second)
    {
        Time = timeLeft = second;

        return this;
    }

    public void begin()
    {
        StopAllCoroutines();
        StartCoroutine(updateTimer());
    }

    private IEnumerator updateTimer()
    {
        if (timeLeft == (Time))
        {
            if (!skipFirst)
                GameObject.Find("GameObject").GetComponent<GameScript>().quantumTick();
            
            else
                skipFirst = false;
        }
        
        while (timeLeft > 0)
        {
            while (paused)
            {
                yield return null; //if paused, skip the rest of the coroutine, ie pause it
            }

            timeLeft--;
            updateUI(timeLeft);
            yield return new WaitForSeconds(1f);
        }
        end();
    }

    public void togglePause()
    {
        paused = !paused;
    }

    private void updateUI(int second)
    {
        //uiText.text = string.Format("{0:D2}:{1:D2}", second / 60, second % 60);
        uiFillImage.fillAmount = Mathf.InverseLerp(0, Time, second);
        //uiFillImage.color = gradient.Evaluate(Mathf.InverseLerp(0, Time, second));
    }

    public void end()
    {
        reset();

    }

    public int getTimeLeft()
    {
        return timeLeft;
    }
    void OnMouseExit()
    {
        transform.Find("canvas").gameObject.SetActive(false);
    }
    // When mouse enter the tooltip will show
    private void OnMouseOver()
    {
        transform.Find("canvas").gameObject.SetActive(true);
        transform.GetComponentInChildren<Text>().text = "Counting down";

    }
}
