using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private float warningtimer;
    private float warningtimerMax;
    private bool playWarningSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += stoveCounter_OnStateChanged;
        stoveCounter.onProgressChange += stoveCounter_onProgressChange;
        warningtimerMax = .5f;
        warningtimer= warningtimerMax;
    }

    private void Update()
    {
        if (playWarningSound)
        {
            warningtimer -= Time.deltaTime;
            if (warningtimer <= 0)
            {
                SoundManager.instance.PlayWarningSound(Camera.main.transform.position,0.3f);
                warningtimer = warningtimerMax;
            }
        }
    }

    private void stoveCounter_onProgressChange(object sender, IHasProgress.OnProgressChangeArgs args)
    {
        float burnShowProgressAmount = 0.5f;
        playWarningSound =stoveCounter.IsFried() && args.progressNormalized>=burnShowProgressAmount;
        
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











