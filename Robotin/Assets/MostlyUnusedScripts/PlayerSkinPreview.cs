using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkinPreview : MonoBehaviour
{
    private Image skinPreview;

    // Start is called before the first frame update
    void Start()
    {
        skinPreview = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        skinPreview.sprite = GameManager_R.instance.playerData.playerSkin;
    }
}
