using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewController : MonoBehaviour
{
    // Define los tipos de screws y sus valores en puntos
    public enum ScrewType { OnePoint, FivePoints }

    [Tooltip("Selecciona el tipo de screw desde el editor")]
    public ScrewType screwType;

    private int screwValue;

    private void Start()
    {
        switch (screwType)
        {
            case ScrewType.OnePoint:
                screwValue = 1;
                break;
            case ScrewType.FivePoints:
                screwValue = 5;
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (GameManager.instance.scoreManager != null)
            {
                GameManager.instance.scoreManager.AddScore(screwValue);
            }

            Destroy(gameObject);
        }

    }
  
}
