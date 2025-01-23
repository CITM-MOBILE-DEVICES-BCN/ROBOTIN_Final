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
            // Play the finish level sound effect
            if (SFXManager.Instance != null)
            {
                SFXManager.Instance.PlayEffect("LevelComplete");
            }
            
            SavePlayerProgress();
            LoadTransition(); 
        }
    }

    private void SavePlayerProgress()
    {
        scoreManager.SaveScore();
        scoreManager.SaveLevel();
        PlayerPrefs.SetString("NextLevel", nextLevelName);
        PlayerPrefs.Save();

    }

    private void LoadTransition()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelTransition");
        }
        else
        {
            Debug.LogError("Next level name is not set!");
        }
    }

}