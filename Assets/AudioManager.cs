using System;
using System.Collections;
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
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private Sound[] _sounds;

    

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
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in _sounds)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
