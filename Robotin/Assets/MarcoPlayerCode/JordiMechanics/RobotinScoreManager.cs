using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RobotinScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore;

    private void Start()
    {
        currentScore = PlayerPrefs.GetInt("PlayerScore", 0);
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
        PlayerPrefs.SetInt("PlayerScore", currentScore);
        PlayerPrefs.Save();
    }

    public void SaveLevel()
    {
        PlayerPrefs.SetString("LastLevel", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
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
