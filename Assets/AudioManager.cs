using System.Collections;
using System;
using System.Collections.Generic;
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

    public int head;
}

[Serializable]
public class SoundEvent
{
    public string name;
    public Sound[] sounds;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private SoundEvent[] _sounds;

    public int voice = 0;

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
                if (e.name.Contains("Voice"))
                {
                    List<Sound> sounds = new List<Sound>();
                    foreach (Sound s in e.sounds)
                    {
                        if (s.head <= voice)
                        {
                            sounds.Add(s);
                        }
                    }

                    if (sounds.Count == 0) { return; }

                    int i = UnityEngine.Random.Range(0, sounds.Count);

                    sounds[i].sauce.Play();

                }
                else
                {
                    e.sounds[0].sauce.Play();
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
