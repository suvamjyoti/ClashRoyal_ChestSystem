using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerBG;
    [SerializeField] private GameObject clock;

    private float waitTime = 0;         //inSeconds
    private float timePassed = 0;
   // private float remainingTime = 0;    //inSeconds


    public void InitialiseTimer(float waitTime)
    {
        this.waitTime = waitTime * 60 * 60;
        //remainingTime = this.waitTime;
        updateTimerText();
        StartCoroutine(initialAnimation());
    }

    private IEnumerator initialAnimation()
    {
        //first we stretch it out
        bool isStretching = true;
        
        while (isStretching)
        {
            if (timerBG.fillAmount < 1)
            {
                timerBG.fillAmount += 0.2f;
            }
            else
            {
                timerBG.fillAmount = 1;
                isStretching = false;
            }

            yield return new WaitForSeconds(0.2f);
        }

        //now show clock
        clock.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        //now show timer text and start
        timerText.gameObject.SetActive(true);

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        bool isTimerRunning = true;

        while (isTimerRunning)
        {

            if (timePassed < waitTime)
            {
                 timePassed++;
                updateTimerText();
            }
            else
            {
                timerBG.gameObject.SetActive(false);
                isTimerRunning = false;
            }

            yield return new WaitForSeconds(1);
        }
    }


    private void updateTimerText()
    {
        timerText.text = getRemainingTime().ToString() + "sec";
    } 

    public float getRemainingTime()
    {
        return waitTime-timePassed;
    }

}
