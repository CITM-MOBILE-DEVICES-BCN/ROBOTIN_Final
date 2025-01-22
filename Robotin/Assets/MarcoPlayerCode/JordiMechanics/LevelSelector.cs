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
        // Obtener el último nivel completado desde PlayerPrefs
        string lastLevelName = PlayerPrefs.GetString("NextLevel", levelSceneNames[0]);

        Debug.Log($"Último nivel completado: {lastLevelName}");

        Button[] buttons = buttonParent.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < levelSceneNames.Length)
            {
                string sceneName = levelSceneNames[i];
                Debug.Log($"Verificando botón para nivel: {sceneName}");

                // Habilitar o deshabilitar el botón según si el nivel está desbloqueado
                if (IsLevelUnlocked(sceneName, lastLevelName))
                {
                    buttons[i].interactable = true;
                    buttons[i].onClick.RemoveAllListeners();
                    buttons[i].onClick.AddListener(() => LoadLevel(sceneName));
                }
                else
                {
                    buttons[i].interactable = false;
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
        PlayerPrefs.SetString("LastLevel", "Level_1_Robotin");
        PlayerPrefs.SetString("NextLevel", "Level_1_Robotin");
        PlayerPrefs.SetInt("PlayerScore", 0);
        PlayerPrefs.Save();

        UpdateLevelButtons();
    }

}
