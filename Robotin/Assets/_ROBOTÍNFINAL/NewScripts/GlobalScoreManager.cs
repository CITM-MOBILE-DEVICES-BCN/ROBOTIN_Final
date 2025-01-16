using UnityEngine;
using TMPro;

public class GlobalScoreManager : MonoBehaviour
{
    public static GlobalScoreManager instance;
    private int globalScore;

    [SerializeField] private TextMeshProUGUI scoreText;

    public void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        LoadGlobalScore();
    }

    public void SumToGlobalScore(int score)
    {
        globalScore += score;
        Debug.Log("Global Score: " + globalScore);

        SaveGlobalScore();
        UpdateScoreText();
    }

    public int GetCurrentGlobalScore()
    {
        return globalScore;
    }

    public void SaveGlobalScore()
    {
        PlayerPrefs.SetInt("GlobalScore", globalScore);
        PlayerPrefs.Save();
    }

    private void LoadGlobalScore()
    {
        globalScore = PlayerPrefs.GetInt("GlobalScore", 0);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Global Score: " + globalScore.ToString();
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned in the inspector!");
        }
    }
}