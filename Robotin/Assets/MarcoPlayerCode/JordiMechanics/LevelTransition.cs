using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerPrefs.GetString("LastLevel"));
    }

    public void LoadNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerPrefs.GetString("NextLevel"));
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RobotinLobby");
    }
}
