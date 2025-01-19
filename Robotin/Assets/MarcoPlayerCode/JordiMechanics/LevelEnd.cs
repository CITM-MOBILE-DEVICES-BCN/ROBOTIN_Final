using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    [SerializeField] private RobotinScoreManager scoreManager;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SavePlayerScore();
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private void SavePlayerScore()
    {
        scoreManager.SaveScore();
    }

    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogError("Next level name is not set!");
        }
    }

}