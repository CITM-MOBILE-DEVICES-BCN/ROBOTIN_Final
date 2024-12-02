using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpOrbController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Respawn());
            gameObject.SetActive(false);
            
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(true);
    }
}
