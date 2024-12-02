using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager 
{
   

    private int currentScore;

    private int highScore;

    public void AddScore(int points)
    {
        currentScore += points;
      GameCanvasUI.instance.UpdateScoreUI();
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }


}
