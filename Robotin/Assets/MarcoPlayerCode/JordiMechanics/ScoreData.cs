using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "Game/ScoreData", order = 1)]
public class ScoreData : ScriptableObject
{
    public int totalScore;

    public void SaveScore(int score)
    {
        totalScore = score;
    }

    public void ResetScore()
    {
        totalScore = 0;
    }
}

