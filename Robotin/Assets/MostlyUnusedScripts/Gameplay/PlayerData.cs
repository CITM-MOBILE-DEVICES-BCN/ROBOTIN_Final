using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_R
{
    public Sprite playerSkin;
    public int currentLevel;

    public PlayerData_R()
    {
        playerSkin = null;
        currentLevel = 1;
    }

    public void SetPlayerSkin(Sprite skin)
    {
        playerSkin = skin;
    }
}
