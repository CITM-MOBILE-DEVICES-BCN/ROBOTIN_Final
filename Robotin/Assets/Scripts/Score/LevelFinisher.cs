using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinisher : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager_R.instance.OnLevelFinished();
            GameManager_R.instance.LoadScene("RobotinMeta");
            GameManager_R.instance.currentLevel.timerManager.ResetTimer();
        }
    }
}
