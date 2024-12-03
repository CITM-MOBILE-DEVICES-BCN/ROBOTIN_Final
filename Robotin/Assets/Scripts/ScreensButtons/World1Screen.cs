using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class World1Screen : MonoBehaviour
{
    [SerializeField] private List<SelectLevelButton> levelButtons;


    private void Awake()
    {
        for (int i = 0; GameManager.instance.gameData.GetNextLevel() > i; i++)
        {
            if(i < levelButtons.Count)
            {
                levelButtons[i].Init(i + 1);
            }
        }

    }

    private void Start()
    {
       

    }

    private void OnBackButtonClicked()
    {
        GameManager.instance.UnLoadScreen(gameObject.name);
    }

   
}
