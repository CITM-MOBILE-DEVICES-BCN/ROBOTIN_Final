using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobotinLevelSelector : MonoBehaviour
{
    public Button[] levelButtons;
    public string[] sceneNames;

    private void Awake()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i;
            levelButtons[i].onClick.AddListener(() => SceneManager.LoadScene(sceneNames[level]));
        }
    }
}
