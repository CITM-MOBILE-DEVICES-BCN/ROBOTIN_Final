using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoubleJumpOrbController : MonoBehaviour
{
    public AudioClip deactivateSound; 
    private AudioSource audioSource;

    private void Awake()
    {
        // Añade o usa un AudioSource en el GameObject.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        // Reproduce el sonido antes de desactivar el objeto.
        if (deactivateSound != null)
        {
            audioSource.PlayOneShot(deactivateSound);
        }

        // Desactiva el objeto.
        gameObject.SetActive(false);

        // Espera 5 segundos antes de reactivarlo.
        yield return new WaitForSeconds(5);
        gameObject.SetActive(true);
    }
}
