using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class RobotinTimer : MonoBehaviour
{
    [Header("Level Timer Settings")]
    [SerializeField] private float levelTimeLimit = 120.0f;
    private float timeRemaining;
    private bool isTimerRunning = false;

    [Header("UI References")]
    [SerializeField] private string timerTextName = "TimerText";
    private TextMeshProUGUI timerText;
    [SerializeField] private Color defaultTextColor = Color.white;
    [SerializeField] private Color warningTextColor = Color.red;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();

        ResetLevelTimer();
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            DisplayTime();

            if (timeRemaining <= 0)
            {
                TimeUp();
            }
        }
    }

    private void DisplayTime()
    {
        if (timerText == null)
        {
            Debug.LogWarning("TimerText is not assigned in the inspector.");
            return;
        }

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeRemaining <= 10f)
        {
            timerText.color = warningTextColor;
        }
        else
        {
            timerText.color = defaultTextColor;
        }
    }

    private void TimeUp()
    {
        isTimerRunning = false;

        Debug.Log("Time's up!");
        ResetLevel();
    }

    public void ResetLevelTimer()
    {
        timeRemaining = levelTimeLimit;
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }


}
