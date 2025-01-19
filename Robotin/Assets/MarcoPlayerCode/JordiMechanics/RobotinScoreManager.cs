using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RobotinScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;

    private void Start()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public void SaveScore()
    {
        scoreData.SaveScore(currentScore);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }

    private void OnDestroy()
    {
        SaveScore();
    }
}
