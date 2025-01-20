using UnityEngine;
using System.Collections.Generic;

public class EnvironmentSFXManager : MonoBehaviour
{
    public static EnvironmentSFXManager Instance { get; private set; }

    [SerializeField] private List<EnvironmentSoundGroup> soundGroups = new List<EnvironmentSoundGroup>();
    private Dictionary<string, EnvironmentSoundGroup> groupDictionary = new Dictionary<string, EnvironmentSoundGroup>();
    private Dictionary<EnvironmentSoundGroup, float> nextGroupPlayTime = new Dictionary<EnvironmentSoundGroup, float>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            InitializeGroups();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeGroups()
    {
        groupDictionary.Clear();
        nextGroupPlayTime.Clear();
        foreach (var group in soundGroups)
        {
            if (group != null && !string.IsNullOrEmpty(group.groupName))
            {
                groupDictionary[group.groupName] = group;
                nextGroupPlayTime[group] = 0f;
            }
        }
    }
    
    private void InitializeAudioSources()
    {
        foreach (var group in soundGroups)
        {
            if (group == null) continue;
            
            foreach (var soundEntry in group.sounds)
            {
                soundEntry.source = gameObject.AddComponent<AudioSource>();
                soundEntry.source.clip = soundEntry.clip;
                soundEntry.source.volume = soundEntry.volume;
                soundEntry.source.loop = false; // We'll handle looping ourselves
                soundEntry.source.playOnAwake = false;
                
                if (soundEntry.playOnStart)
                {
                    PlayEnvironmentSound(group, soundEntry.soundName);
                }
            }
        }
    }

    private void Update()
    {
        foreach (var currentGroup in soundGroups)
        {
            if (currentGroup == null) continue;

            // Check if any sound in the group is currently playing
            bool isAnyPlaying = false;
            foreach (var groupSound in currentGroup.sounds)
            {
                if (groupSound.isPlaying && groupSound.source.isPlaying)
                {
                    isAnyPlaying = true;
                    break;
                }
            }

            // If no sound is playing and it's time for the next sound
            if (!isAnyPlaying && Time.time >= nextGroupPlayTime[currentGroup])
            {
                PlayEnvironmentSound(currentGroup);
            }
        }
    }

    public void PlayEnvironmentSound(EnvironmentSoundGroup group)
    {
        if (group == null) return;
        
        // Stop any currently playing sounds in this group
        foreach (var groupSound in group.sounds)
        {
            if (groupSound.isPlaying)
            {
                StopSound(groupSound);
            }
        }

        var nextSound = group.GetNextSound();
        if (nextSound != null)
        {
            PlaySound(nextSound, group);
        }
    }

    public void PlayEnvironmentSound(string groupName)
    {
        if (groupDictionary.TryGetValue(groupName, out var group))
        {
            PlayEnvironmentSound(group);
        }
    }

    public void PlayEnvironmentSound(EnvironmentSoundGroup group, string soundName)
    {
        if (group == null) return;
        var foundSound = group.GetSpecificSound(soundName);
        if (foundSound != null)
        {
            PlaySound(foundSound, group);
        }
    }

    private void PlaySound(EnvironmentSoundGroup.EnvironmentSound soundToPlay, EnvironmentSoundGroup group)
    {
        if (soundToPlay == null || soundToPlay.source == null) return;

        // Stop any currently playing sounds in this group
        foreach (var groupSound in group.sounds)
        {
            if (groupSound != soundToPlay && groupSound.isPlaying)
            {
                StopSound(groupSound);
            }
        }

        soundToPlay.isPlaying = true;
        soundToPlay.source.Play();

        // Schedule next play time based on clip length and interval
        float clipLength = soundToPlay.clip != null ? soundToPlay.clip.length : 0f;
        nextGroupPlayTime[group] = Time.time + clipLength + soundToPlay.playInterval;
    }

    private void PlaySoundDirectly(EnvironmentSoundGroup.EnvironmentSound soundToPlay)
    {
        if (soundToPlay == null || soundToPlay.source == null) return;
        soundToPlay.source.Play();
    }

    public void StopEnvironmentSound(EnvironmentSoundGroup group, string soundName)
    {
        if (group == null) return;
        var foundSound = group.GetSpecificSound(soundName);
        if (foundSound != null)
        {
            StopSound(foundSound);
        }
    }

    public void StopEnvironmentSound(string groupName, string soundName)
    {
        if (groupDictionary.TryGetValue(groupName, out var group))
        {
            StopEnvironmentSound(group, soundName);
        }
    }

    private void StopSound(EnvironmentSoundGroup.EnvironmentSound soundToStop)
    {
        if (soundToStop == null || soundToStop.source == null) return;
        
        soundToStop.isPlaying = false;
        soundToStop.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (var group in soundGroups)
        {
            if (group == null) continue;
            
            foreach (var groupSound in group.sounds)
            {
                if (groupSound.isPlaying)
                {
                    StopSound(groupSound);
                }
            }
        }
    }

    private void OnDisable()
    {
        StopAllSounds();
    }
} 