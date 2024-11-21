using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World1Screen : MonoBehaviour
{
    [SerializeField] Button backButton;
    [SerializeField] Button level1Button;

    private void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        level1Button.onClick.AddListener(OnLevel1Clicked);
    }

    private void OnBackButtonClicked()
    {
        GameManager.instance.UnLoadScreen(gameObject.name);
    }

    private void OnLevel1Clicked()
    {
        GameManager.instance.LoadScene("RobotinGame");
    }
}
