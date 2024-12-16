using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenuPopUp : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Button exitButton;
    private void Start()
    {
        continueButton.onClick.AddListener(OnPauseButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnPauseButtonClicked()
    {

        GameManager_R.instance.UnLoadPopUp(gameObject.name);
        GameManager_R.instance.currentLevel.timerManager.PauseResumeTimer();
        //You can add a resume game function here
    }

    private void OnExitButtonClicked()
    {
        Time.timeScale = 1;
        GameManager_R.instance.LoadScene("RobotinMeta");
        GameManager_R.instance.currentLevel.timerManager.ResetTimer();
    }
}
