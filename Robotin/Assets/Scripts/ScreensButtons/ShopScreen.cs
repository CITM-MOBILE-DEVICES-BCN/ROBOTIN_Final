using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField]Button backButton;
    [SerializeField]Button itemExample;
    private void Start()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        itemExample.onClick.AddListener(OnItemExampleClicked);
    }

    private void OnBackButtonClicked()
    {
        GameManager.instance.UnLoadScreen(gameObject);
    }

    private void OnItemExampleClicked()
    {
        // Do something
    }
}
