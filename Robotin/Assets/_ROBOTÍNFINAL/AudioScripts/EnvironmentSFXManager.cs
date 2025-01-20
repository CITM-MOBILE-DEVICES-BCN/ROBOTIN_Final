using UnityEngine;
using System.Collections.Generic;

public class EnvironmentSFXManager : MonoBehaviour
{
    public static EnvironmentSFXManager Instance { get; private set; }

    [SerializeField] private List<EnvironmentSoundGroup> soundGroups = new List<EnvironmentSoundGroup>();
    private Dictionary<string, EnvironmentSoundGroup> groupDictionary = new Dictionary<string, EnvironmentSoundGroup>();

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
        foreach (var group in soundGroups)
        {
            if (group != null && !string.IsNullOrEmpty(group.groupName))
            {
                groupDictionary[group.groupName] = group;
            }
        }
    }
    
    private void InitializeAudioSources()
    {
        foreach (var group in soundGroups)
        {
            if (group == null) continue;
            
            foreach (var sound in group.sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.loop = sound.loop;
                sound.source.playOnAwake = false;
                
                if (sound.playOnStart)
                {
                    PlayEnvironmentSound(group, sound.soundName);
                }
            }
        }
    }

    private void Update()
    {
        // Handle interval-based sounds
        foreach (var group in soundGroups)
        {
            if (group == null) continue;
            
            foreach (var sound in group.sounds)
            {
                if (sound.isPlaying && sound.playInterval > 0)
                {
                    if (Time.time >= sound.nextPlayTime)
                    {
                        PlaySoundDirectly(sound);
                    }
                }
            }
        }
    }

    public void PlayEnvironmentSound(EnvironmentSoundGroup group)
    {
        if (group == null) return;
        
        var sound = group.GetNextSound();
        if (sound != null)
        {
            PlaySound(sound);
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
        var sound = group.GetSpecificSound(soundName);
        if (sound != null)
        {
            PlaySound(sound);
        }
    }

    private void PlaySound(EnvironmentSoundGroup.EnvironmentSound sound)
    {
        if (sound == null || sound.source == null) return;

        sound.isPlaying = true;
        PlaySoundDirectly(sound);
    }

    private void PlaySoundDirectly(EnvironmentSoundGroup.EnvironmentSound sound)
    {
        if (sound.playInterval > 0)
        {
            sound.nextPlayTime = Time.time + sound.playInterval;
        }
        
        sound.source.Play();
    }

    public void StopEnvironmentSound(EnvironmentSoundGroup group, string soundName)
    {
        if (group == null) return;
        var sound = group.GetSpecificSound(soundName);
        if (sound != null)
        {
            StopSound(sound);
        }
    }

    public void StopEnvironmentSound(string groupName, string soundName)
    {
        if (groupDictionary.TryGetValue(groupName, out var group))
        {
            StopEnvironmentSound(group, soundName);
        }
    }

    private void StopSound(EnvironmentSoundGroup.EnvironmentSound sound)
    {
        if (sound == null || sound.source == null) return;
        
        sound.isPlaying = false;
        sound.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (var group in soundGroups)
        {
            if (group == null) continue;
            
            foreach (var sound in group.sounds)
            {
                if (sound.isPlaying)
                {
                    StopSound(sound);
                }
            }
        }
    }

    private void OnDisable()
    {
        StopAllSounds();
    }
} 