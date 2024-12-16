using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyScreen : MonoBehaviour
{
    [SerializeField] Button RobotinButton;
    [SerializeField] Button WaltersButton;

    private void Start()
    {
        RobotinButton.onClick.AddListener(OnRobotinButtonClicked);
        WaltersButton.onClick.AddListener(OnWaltersButtonClicked);
    }

    private void OnRobotinButtonClicked()
    {
        GameManager_R.instance.LoadScene("RobotinMeta");
    }

    private void OnWaltersButtonClicked()
    {
        GameManager_R.instance.LoadScene("Meta");
    }
}
