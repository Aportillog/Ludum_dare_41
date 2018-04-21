using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;
public class AudioController : MonoBehaviour {

    public Sound[] sounds;
    public bool isMute;

    public static AudioController instance;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        isMute = false;
        Play("MusicAmbient");
    }

    public void Play(string name)
    {
        if(!isMute)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            if (s.source == null)
            {
                Debug.LogWarning("Audio source for: " + name + " not found!");
                return;
            }
            s.source.Play();
        }
    }

    public void switchVolumeOnOf()
    {
        isMute = !isMute;
    }

}
