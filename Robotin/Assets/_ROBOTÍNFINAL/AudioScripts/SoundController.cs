using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private float musicStartDelay = 2f;
    [SerializeField] private string ambientTrackGroup = "AmbientTrackGroup1";
    
    [Header("Sound Settings")]
    [SerializeField] private float masterVolume = 1f;
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;

    private void Start()
    {
        // Initialize volumes
        if (SoundTrackManager.Instance != null)
        {
            SoundTrackManager.Instance.SetMasterVolume(musicVolume);
        }
        
        if (SFXManager.Instance != null)
        {
            // Assuming SFXManager has volume control
            // SFXManager.Instance.SetVolume(sfxVolume);
        }

        // Start ambient music with delay
        StartCoroutine(StartAmbientMusic());
    }

    private IEnumerator StartAmbientMusic()
    {
        yield return new WaitForSeconds(musicStartDelay);
        PlayAmbientMusic();
    }

    public void PlayAmbientMusic()
    {
        if (SoundTrackManager.Instance != null)
        {
            SoundTrackManager.Instance.PlayMusic(ambientTrackGroup);
        }
    }

    public void StopAmbientMusic()
    {
        if (SoundTrackManager.Instance != null)
        {
            SoundTrackManager.Instance.StopMusic();
        }
    }

    // Volume Control Methods
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (SoundTrackManager.Instance != null)
        {
            SoundTrackManager.Instance.SetMasterVolume(musicVolume * masterVolume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (SFXManager.Instance != null)
        {
            // Assuming SFXManager has volume control
            // SFXManager.Instance.SetVolume(sfxVolume * masterVolume);
        }
    }

    // Game-specific sound methods
    public void PlayVictoryMusic()
    {
        if (SoundTrackManager.Instance != null)
        {
            SoundTrackManager.Instance.PlayMusic("VictoryTrack");
        }
    }

    public void PlayGameOverMusic()
    {
        if (SoundTrackManager.Instance != null)
        {
            SoundTrackManager.Instance.PlayMusic("GameOverTrack");
        }
    }

    // Add more game-specific sound methods as needed
} 