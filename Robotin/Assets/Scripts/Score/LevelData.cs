using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private const string ScoreKey = "Score";
    private const string TotalScoreKey = "TotalScore";

    private int score;
    private int total;
    public void Save()
    {
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.SetInt(TotalScoreKey, total);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        score = PlayerPrefs.GetInt(ScoreKey, 0);
        total = PlayerPrefs.GetInt(TotalScoreKey, 0);
    }
    public int GetScore()
    {
        return score;
    }

    public int GetTotal()
    {
        return total;
    }
    //public void SetScore(int value)
    //{
    //    GameManager.instance.gameData.score = value;
    //    scoreText.text = GameManager.instance.gameData.GetScore().ToString();
    //}
    public void SetTotalScore(int value)
    {
        total = value;
        //bestText.text = GameManager.instance.gameData.GetBest().ToString();
    }
 
}
