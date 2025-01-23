using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyScreen : MonoBehaviour
{
    [SerializeField] Button RobotinButton;
    [SerializeField] Button WaltersButton;
    [SerializeField] Button KyotoButton;
    [SerializeField] Button RuinButton;

    private void Start()
    {
        RobotinButton.onClick.AddListener(OnRobotinButtonClicked);
        WaltersButton.onClick.AddListener(OnWaltersButtonClicked);
        KyotoButton.onClick.AddListener(OnKyotoButtonClicked);
        RuinButton.onClick.AddListener(OnRuinButtonClicked);
    }

    private void OnRobotinButtonClicked()
    {
        GameManager_R.instance.LoadScene("RobotinMeta");
    }

    private void OnWaltersButtonClicked()
    {
        GameManager_R.instance.LoadScene("Meta");
    }

    private void OnKyotoButtonClicked()
    {
        GameManager_R.instance.LoadScene("KyotoMeta");
    }

    private void OnRuinButtonClicked()
    {
        GameManager_R.instance.LoadScene("3.Meta");
    }
}
