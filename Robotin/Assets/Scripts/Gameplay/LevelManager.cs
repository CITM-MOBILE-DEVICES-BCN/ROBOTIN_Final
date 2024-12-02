using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimerModule;
using System;
using TimerSampleScene;

public class LevelManager : MonoBehaviour
{
    private Timer timer;
    private IDateTimeProvider dateTimeProvider;
    private TimerService timerService;

    public TimerViewValues timerViewValues;


    //TODO: Implement an state machine that will handle the pause state
    private bool isOnPause = false;

    // Start is called before the first frame update
    void Start()
    {
        dateTimeProvider = new DateTimeProvider();
        timerService = new TimerService(dateTimeProvider);        

        timer = new Timer(TimeSpan.FromSeconds(0));

        float duration = 30;
        timerService.StartTimer(timer, TimeSpan.FromSeconds(duration));
    }

    private void Update()
    {
        if (timer != null)
        {
            var elapsedTime = timerService.GetTimerElapsedTime(timer);

            if (elapsedTime >= timer.Duration)
            {
                timerService.StopTimer(timer);
            }

            if (timerService.IsTimerDefrosted(timer))
            {
                timerService.DefrostTimer(timer);
            }

            timerViewValues.UpdateView(timer, timerService);
        }
    }

    private float GetTimerRemainingTimeNormalized(Timer timer)
    {
        return (float)(timerService.GetTimerElapsedTime(timer).TotalSeconds / timer.Duration.TotalSeconds);
    }

    //Called On Level Passed
    public void ResetTimer()
    {
        timerService.ResetTimer(ref timer);
    }
    public void PauseResumeTimer()
    {
        if (!isOnPause)
        {
            timerService.PauseTimer(timer);
            isOnPause = true;

        }
        else
        {
            timerService.ResumeTimer(timer);
            isOnPause = false;
        }

    }

}
