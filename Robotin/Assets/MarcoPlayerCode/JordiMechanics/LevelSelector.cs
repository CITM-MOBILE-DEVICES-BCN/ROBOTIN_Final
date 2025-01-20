using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MyNavigationSystem;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject buttonParent;
    [SerializeField] private string[] levelSceneNames;
    [SerializeField] private Button resetProgressButton;

    private void Start()
    {
        UpdateLevelButtons();

        if (resetProgressButton != null)
        {
            resetProgressButton.onClick.AddListener(ResetProgress);
        }
    }

    private void UpdateLevelButtons()
    {
        string highestUnlockedLevelName = PlayerPrefs.GetString("HighestUnlockedLevel", levelSceneNames[0]);

        Button[] buttons = buttonParent.GetComponentsInChildren<Button>();
        Debug.Log($"Nivel desbloqueado más alto: {highestUnlockedLevelName}");

        for (int i=0; i < buttons.Length; i++)
        {
            Debug.Log($"Nivel {i}: {levelSceneNames[i]}");
            if (i < levelSceneNames.Length)
            {
                string sceneName = levelSceneNames[i];

                if (IsLevelUnlocked(sceneName, highestUnlockedLevelName))
                {
                    buttons[i].interactable = true;
                    buttons[i].onClick.RemoveAllListeners();
                    buttons[i].onClick.AddListener(() => LoadLevel(sceneName));
                }
                else
                {
                    buttons[i].interactable = false;

                    // Change button sprite
                }
            }
            
        }
    }

    private bool IsLevelUnlocked(string sceneName, string highestUnlockedLevelName)
    {
        int sceneIndex = System.Array.IndexOf(levelSceneNames, sceneName);
        int unlockedIndex = System.Array.IndexOf(levelSceneNames, highestUnlockedLevelName);
        return sceneIndex <= unlockedIndex;
    }

    private void LoadLevel(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("HighestUnlockedLevel", 0);
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.Save();

        UpdateLevelButtons();
    }

}
