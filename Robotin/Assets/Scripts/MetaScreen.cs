using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MetaScreen : MonoBehaviour
{
    [SerializeField] Button shopButton;

    [SerializeField] GameObject shopScreen;


    private void Start()
    {
        shopButton.onClick.AddListener(OnShopButtonClicked);
    }

    private void OnShopButtonClicked()
    {
        GameManager.instance.LoadScreen(shopScreen);
    }
}
