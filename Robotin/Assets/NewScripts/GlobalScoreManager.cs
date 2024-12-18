using System.Collections;
using System.Collections.Generic;
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

        globalScore = 0;
    }

    public void SumToGlobalScore(int score)
    {
        globalScore += score;
        Debug.Log("Global Score: " + globalScore);
    }

    public int GetCurrentGlobalScore()
    {
        return globalScore;
    }
}
