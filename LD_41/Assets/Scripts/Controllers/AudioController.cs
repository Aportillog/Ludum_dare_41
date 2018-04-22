using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // called first
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //sceneLoaded = scene.name;

        stopAllSounds();

        if (scene.name == "Main")
        {
            Play("MainMusic");
        }
        else if (scene.name == "Menu")
        {
            Play("MenuMusic");
        }
    }

    void Start()
    {
        isMute = false;
    }

    public void Play(string name)
    {
        if(!isMute)
        {
            findSource(name).source.Play();
        }
    }

    private void Stop(string name)
    {
        findSource(name).source.Stop();
    }

    private void stopAllSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    private void toogleMuteAllSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = !s.source.mute;
        }
    }

    public void muteGame()
    {
        isMute = !isMute;
        toogleMuteAllSounds();
    }

    private Sound findSource(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        if (s.source == null)
        {
            Debug.LogWarning("Audio source for: " + name + " not found!");
        }

        return s;
    }

}
