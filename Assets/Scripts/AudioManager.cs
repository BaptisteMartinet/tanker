using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1;
        [Range(0f, 1f)]
        public float pitch = 1;
        public bool loop;

        public bool isGlobal = false;

        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    private void Awake()
    {
        Array.ForEach(sounds, s => {
            if (s.isGlobal)
                s.source = this.createSourceInstance(s.name, this.gameObject);
        });
    }

    private void Start()
    {
        this.Play("main_theme");
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound?.source == null)
        {
            Debug.LogWarning($"Invalid sound name: {name}");
            return;
        }
        sound.source.Play();
    }

    public AudioSource createSourceInstance(string name, GameObject obj)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Invalid sound name: {name}");
            return null;
        }
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.loop = sound.loop;
        source.spatialBlend = 1;
        return source;
    }
}
