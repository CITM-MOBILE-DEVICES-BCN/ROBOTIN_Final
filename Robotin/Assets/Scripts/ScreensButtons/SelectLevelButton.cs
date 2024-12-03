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
        if (level <= GameManager.instance.gameData.GetNextLevel())
        {
            levelButton.interactable = true;
            levelScoreText.text = GameManager.instance.gameData.GetHighScoreFromLevel(level - 1).ToString();
        }
        else
        {
            levelButton.interactable = false;
        }

        levelNameText.text = "Level " + level;
        levelButton.onClick.AddListener(() => OnLevelClicked(level));
        
    }
    private void OnLevelClicked(int level)
    {
        GameManager.instance.LoadSceneAndLevel("RobotinGame", level);
    }
}
