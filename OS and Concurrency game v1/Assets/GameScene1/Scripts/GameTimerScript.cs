using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimerScript : MonoBehaviour
{

    [SerializeField] private TMP_Text uiText;

    private int timeCount;
    private bool paused = false; //track whether the clock is paused
    [SerializeField] Sprite playButton; //set in the inspector
    [SerializeField] Sprite pauseButton; //set in the inspector

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
        
        while (timeCount < 300)
        {
            while (paused)
            {
                yield return null; //if paused, skip the rest of the coroutine, ie pause it
            }
            //find the gamescript and call OnTick
            //coupling moment, this is definitely not good code practice but what can ya do
            GameObject.Find("GameObject").GetComponent<GameScript>().onTick();

            UpdateUI(timeCount);
            timeCount++;
            //Debug.Log(timeCount);
            yield return new WaitForSeconds(1f);
        }

        End();
    }

    public void togglePause()
    {
        if (!paused)
        {
            transform.GetChild(1).gameObject.GetComponent<Image>().sprite = pauseButton; //set the button sprite to pause
            GameObject.Find("TopBelt").GetComponent<BeltScript>().SetScrollSpeedX(0f); //stop the belt animation
            GameObject.Find("BottomBelt").GetComponent<BeltScript>().SetScrollSpeedX(0f);
            GameObject.Find("TopBelt").GetComponent<AudioSource>().volume = 0f; //stop belt sound
            //uiText.color = new Color(52, 250, 0, 255);
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponent<Image>().sprite = playButton; //set the button sprite to play
            GameObject.Find("TopBelt").GetComponent<BeltScript>().SetScrollSpeedX(0.15f); //start the belt animation
            GameObject.Find("BottomBelt").GetComponent<BeltScript>().SetScrollSpeedX(0.15f);
            GameObject.Find("TopBelt").GetComponent<AudioSource>().volume = 0.05f; //start belt sound
            //uiText.color = new Color(250, 124, 0, 255);
        }

        paused = !paused;
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
