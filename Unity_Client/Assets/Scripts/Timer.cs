using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion;

    // Flag for use extend time powerup
    public bool useExtendTime;
    
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    public void ActivateExtendTime()
    {
        useExtendTime = true;
    }

    public void ResetTime()
    {
        timeToCompleteQuestion = 30f;
    }


    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        // Activate the extend time powerup
        if (useExtendTime)
        {
            timerValue += 5f;
            useExtendTime = false;
        }


        if(isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                
                fillFraction = timerValue / timeToCompleteQuestion;
           
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
