using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    [SerializeField] Button levelButton;
    [SerializeField] TextMeshProUGUI levelNameText;
    [SerializeField] TextMeshProUGUI levelScoreText;
    private int level;
    public void Init(int level)
    {
        this.level = level;
        if (level <= GameManager_R.instance.gameData.GetNextLevel())
        {
            levelButton.interactable = true;
            levelScoreText.gameObject.SetActive(true);
            levelScoreText.text = GameManager_R.instance.gameData.GetHighScoreFromLevel(level - 1).ToString();
            levelNameText.text = "Level " + level;
        }
        else
        {
            levelNameText.text = "Level Blocked";
            levelScoreText.gameObject.SetActive(false);
            levelButton.interactable = false;
        }

        
        levelButton.onClick.AddListener(() => OnLevelClicked(level));
        
    }
    private void OnLevelClicked(int level)
    {
        GameManager_R.instance.LoadSceneAndLevel("RobotinGame", level);
    }
}
