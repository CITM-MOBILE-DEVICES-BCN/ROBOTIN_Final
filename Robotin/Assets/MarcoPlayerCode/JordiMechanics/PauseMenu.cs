using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button pauseButton;

    private bool isPaused = false;

    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayEffect("ButtonClick");
        }
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayEffect("ButtonClick");
        }
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.PlayEffect("ButtonClick");
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("RobotinMeta");
    }

    private void OnPauseButtonClick()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
}
