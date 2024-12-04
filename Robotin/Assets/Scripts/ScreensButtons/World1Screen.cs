using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class World1Screen : MonoBehaviour
{
    [SerializeField] private List<SelectLevelButton> levelButtons;
    [SerializeField] private Button backButton;


    private void Awake()
    {
        for (int i = 0; levelButtons.Count > i; i++)
        {
            levelButtons[i].Init(i + 1);
        }
        backButton.onClick.AddListener(OnBackButtonClicked);

    }

    private void Start()
    {
       

    }

    private void OnBackButtonClicked()
    {
        GameManager.instance.UnLoadScreen(gameObject.name);
    }

   
}
