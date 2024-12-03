using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class World1Screen : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Button level1Button;
    [SerializeField] Button hardLevel1Button;
    [SerializeField] Button level2Button;
    [SerializeField] Button hardLevel2Button;
    [SerializeField] Button level3Button;
    [SerializeField] Button hardLevel3Button;
    [SerializeField] TextMeshProUGUI level1ScoreText;
    [SerializeField] TextMeshProUGUI level2ScoreText;
    [SerializeField] TextMeshProUGUI level3ScoreText;


    private void Awake()
    {
        hardLevel1Button.interactable = false;
        hardLevel2Button.interactable = false;
        hardLevel3Button.interactable = false;
        level2Button.interactable = false;
        level3Button.interactable = false;

    }

    private void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        level1Button.onClick.AddListener(() => OnLevelClicked(1));
        if (GameManager.instance.gameData.GetNextLevel() > 1)
        {
            level2Button.interactable = true;
            level2Button.onClick.AddListener(() => OnLevelClicked(2));
        }
        if(GameManager.instance.gameData.GetNextLevel() > 2)
        {
            level3Button.interactable = true;
            level3Button.onClick.AddListener(() => OnLevelClicked(3));
        }
        if (GameManager.instance.gameData.GetNextLevel() > 3)
        {
            hardLevel1Button.interactable = true;
            hardLevel1Button.onClick.AddListener(() => OnLevelClicked(4));
        }
        if (GameManager.instance.gameData.GetNextLevel() > 4)
        {
            hardLevel2Button.interactable = true;
            hardLevel2Button.onClick.AddListener(() => OnLevelClicked(5));
        }
        if (GameManager.instance.gameData.GetNextLevel() > 5)
        {
            hardLevel3Button.interactable = true;
            hardLevel3Button.onClick.AddListener(() => OnLevelClicked(6));
        }

        level1ScoreText.text = GameManager.instance.gameData.GetHighScoreFromLevel(0).ToString();
        level2ScoreText.text = GameManager.instance.gameData.GetHighScoreFromLevel(1).ToString();
        level3ScoreText.text = GameManager.instance.gameData.GetHighScoreFromLevel(2).ToString();

    }

    private void OnBackButtonClicked()
    {
        GameManager.instance.UnLoadScreen(gameObject.name);
    }

    private void OnLevelClicked(int level)
    {
        GameManager.instance.LoadSceneAndLevel("RobotinGame", level);
    }
}
