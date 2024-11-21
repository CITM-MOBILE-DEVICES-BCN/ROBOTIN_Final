using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasUI : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] string pauseMenuPopUp;

    private void Start()
    {
        pauseButton.onClick.AddListener(OnPauseButtonClicked);
    }

    private void OnPauseButtonClicked()
    {
        GameManager.instance.LoadPopUp(pauseMenuPopUp);
        //You can add a pause game function here and if you want the same button to resume the
        //game create a bool so it unloads the popup and resumes the game
    }
}
