using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    
    public static MusicManager instance{get; private set;}
    private float volume;
    private AudioSource audioSource;
    private const string MUSIC_VOLUME = "MusicVolume";
    

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        volume= PlayerPrefs.GetFloat(MUSIC_VOLUME,1f);
        audioSource.volume = volume;
    }

    public void ChangeVolume(float volumeChangeCount)
    {
        volume+=volumeChangeCount;
        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
    }
    public float GetVolume()
    {
        return volume;
    }
}
