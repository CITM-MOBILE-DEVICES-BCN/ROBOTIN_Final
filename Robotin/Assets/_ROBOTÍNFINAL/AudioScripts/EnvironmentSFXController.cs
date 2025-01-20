using UnityEngine;

public class EnvironmentSFXController : MonoBehaviour
{
    [System.Serializable]
    public class EnvironmentSoundEntry
    {
        public string soundName;
        public string soundGroup;
        [Range(0f, 1f)]
        public float volumeMultiplier = 1f;
        public bool playOnStart;
        public bool loop;
        [HideInInspector]
        public bool isPlaying;
        [Tooltip("Time to wait before playing the sound again")]
        public float playInterval = 0f;
        [HideInInspector]
        public float nextPlayTime;
    }

    [SerializeField] private EnvironmentSoundEntry[] environmentSounds;
    
    private void Start()
    {
        // Play sounds marked as playOnStart
        for (int i = 0; i < environmentSounds.Length; i++)
        {
            if (environmentSounds[i].playOnStart)
            {
                PlayEnvironmentSound(i);
            }
        }
    }

    private void Update()
    {
        // Handle looping sounds with intervals
        for (int i = 0; i < environmentSounds.Length; i++)
        {
            if (environmentSounds[i].loop && environmentSounds[i].isPlaying && environmentSounds[i].playInterval > 0)
            {
                if (Time.time >= environmentSounds[i].nextPlayTime)
                {
                    PlayEnvironmentSoundDirectly(i);
                }
            }
        }
    }

    public void PlayEnvironmentSound(int index)
    {
        if (index < 0 || index >= environmentSounds.Length) return;
        
        var sound = environmentSounds[index];
        
        if (sound.loop)
        {
            if (!sound.isPlaying)
            {
                sound.isPlaying = true;
                PlayEnvironmentSoundDirectly(index);
            }
        }
        else
        {
            PlayEnvironmentSoundDirectly(index);
        }
    }

    private void PlayEnvironmentSoundDirectly(int index)
    {
        var sound = environmentSounds[index];
        if (sound.loop)
        {
            // For looping sounds with intervals, schedule next play
            if (sound.playInterval > 0)
            {
                sound.nextPlayTime = Time.time + sound.playInterval;
            }
        }
        
        // Use specific sound name if provided, otherwise use group for random variation
        if (!string.IsNullOrEmpty(sound.soundName))
        {
            SFXManager.Instance.PlaySpecificEffect(sound.soundGroup, sound.soundName, sound.volumeMultiplier);
        }
        else
        {
            SFXManager.Instance.PlayEffect(sound.soundGroup, sound.volumeMultiplier);
        }
    }

    public void PlayEnvironmentSound(string soundName)
    {
        for (int i = 0; i < environmentSounds.Length; i++)
        {
            if (environmentSounds[i].soundName == soundName)
            {
                PlayEnvironmentSound(i);
                return;
            }
        }
    }

    public void StopEnvironmentSound(int index)
    {
        if (index < 0 || index >= environmentSounds.Length) return;
        
        var sound = environmentSounds[index];
        if (sound.isPlaying)
        {
            sound.isPlaying = false;
            if (!string.IsNullOrEmpty(sound.soundName))
            {
                SFXManager.Instance.StopEffect(sound.soundGroup, sound.soundName);
            }
        }
    }

    public void StopEnvironmentSound(string soundName)
    {
        for (int i = 0; i < environmentSounds.Length; i++)
        {
            if (environmentSounds[i].soundName == soundName)
            {
                StopEnvironmentSound(i);
                return;
            }
        }
    }

    public void StopAllEnvironmentSounds()
    {
        for (int i = 0; i < environmentSounds.Length; i++)
        {
            if (environmentSounds[i].isPlaying)
            {
                StopEnvironmentSound(i);
            }
        }
    }

    private void OnDisable()
    {
        StopAllEnvironmentSounds();
    }
} 