using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollectible : MonoBehaviour
{
    [Header("Star Settings")]
    [SerializeField] private int scoreValue = 10;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var scoreManager = FindObjectOfType<RobotinScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }
            SFXManager.Instance.PlayEffect("Star");

            // efectos

            Destroy(gameObject);
        }
    }

}
