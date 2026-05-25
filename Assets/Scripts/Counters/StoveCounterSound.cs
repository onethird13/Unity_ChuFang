using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += stoveCounter_OnStateChanged;
    }

    private void stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        Debug.Log("开始");
        bool playSound = (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried);
        Debug.Log(playSound);
        if (playSound)
        {
            audioSource.Play();
            Debug.Log("开始播放");
        }
        else
        {
            audioSource.Stop();
        }
    }
    
    
    
}











