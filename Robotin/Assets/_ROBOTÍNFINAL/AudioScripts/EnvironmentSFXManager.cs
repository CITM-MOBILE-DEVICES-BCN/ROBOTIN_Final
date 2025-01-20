using UnityEngine;
using System.Collections.Generic;

public class EnvironmentSFXManager : MonoBehaviour
{
    public static EnvironmentSFXManager Instance { get; private set; }

    [SerializeField] private List<SoundGroup> soundGroups = new List<SoundGroup>();
    private Dictionary<string, SoundGroup> groupDictionary = new Dictionary<string, SoundGroup>();

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
            }
        }
    }

    public void PlayEnvironmentSound(SoundGroup group)
    {
        if (group == null) return;
        
        Sound sound = group.playSequentially ? group.GetNextSound() : group.sounds[Random.Range(0, group.sounds.Count)];
        if (sound != null && sound.source != null)
        {
            sound.source.Play();
        }
    }

    public void PlayEnvironmentSound(string groupName)
    {
        if (groupDictionary.TryGetValue(groupName, out var group))
        {
            PlayEnvironmentSound(group);
        }
    }

    public void PlaySpecificSound(SoundGroup group, string soundName)
    {
        if (group == null) return;
        var sound = group.GetSpecificSound(soundName);
        if (sound != null && sound.source != null)
        {
            sound.source.Play();
        }
    }

    public void StopEnvironmentSound(SoundGroup group, string soundName)
    {
        if (group == null) return;
        var sound = group.GetSpecificSound(soundName);
        if (sound != null && sound.source != null)
        {
            sound.source.Stop();
        }
    }

    public void StopAllSounds()
    {
        foreach (var group in soundGroups)
        {
            if (group == null) continue;
            
            foreach (var sound in group.sounds)
            {
                if (sound.source != null)
                {
                    sound.source.Stop();
                }
            }
        }
    }

    private void OnDisable()
    {
        StopAllSounds();
    }
} 