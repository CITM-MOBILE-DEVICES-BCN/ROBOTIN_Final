using UnityEngine;

public class GlobalScoreManager : MonoBehaviour
{
    public static GlobalScoreManager instance;
    private int globalScore;

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
}