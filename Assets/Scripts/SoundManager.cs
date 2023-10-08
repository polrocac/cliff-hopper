using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> audios;

    private AudioSource controlAudio;

    public static SoundManager Instance { get; private set; }

    public AudioSource bgMusic;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        controlAudio = GetComponent<AudioSource>();
    }



    public void SelectAudio(int index, float vol)
    {
        controlAudio.PlayOneShot(audios[index], vol);
    }

    public bool AudioIsPlaying()
    {
        return controlAudio.isPlaying;
    }
    
    public AudioClip AudioClip()
    {
        return controlAudio.clip;
    }

    public void Pause()
    {
        bgMusic.Pause();
        controlAudio.Pause();
    }

    public void Resume()
    {
        bgMusic.UnPause();
        controlAudio.UnPause();
    }
}
