using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public float volume;
    public float pitch;
    public bool loop;

    [HideInInspector] public AudioSource sauce;
}

[Serializable]
public class SoundEvent
{
    public string name;
    public Sound[] sounds;
    [HideInInspector] public int soundIndex = 0;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private SoundEvent[] _sounds;


    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (SoundEvent e in _sounds)
        {
            foreach (Sound s in e.sounds)
            {
                s.sauce = gameObject.AddComponent<AudioSource>();
                s.sauce.clip = s.clip; s.sauce.volume = s.volume; s.sauce.pitch = s.pitch; s.sauce.loop = s.loop;
            }
        }
    }

    void Start()
    {

        Play("SongMain");
        Play("Rain");
    }



    public void Play(string name)
    {
        foreach (SoundEvent e in _sounds)
        {
            if (e.name == name)
            {
                Sound s = e.sounds[e.soundIndex];
                s.sauce.Play();

                e.soundIndex++;
                if (e.soundIndex == e.sounds.Length) { 
                    e.soundIndex = 0; 
                }
            }
        }
    }

    public void Stop(string name)
    {
        foreach (SoundEvent e in _sounds)
        {
            if (e.name == name)
            {
                foreach (Sound s in e.sounds)
                {
                    s.sauce.Stop();
                }
            }
        }
    }


}
