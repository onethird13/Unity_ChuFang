using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;
    private float footStepTimer;
    private float maxFootSteptime;

    private void Awake()
    {
        maxFootSteptime = 0.3f;
        player = GetComponent<Player>();
        footStepTimer = 0;
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (footStepTimer <= 0 && player.IsWalking())
        {
            footStepTimer = maxFootSteptime;
            SoundManager.instance.PlayFootStepSound(Camera.main.transform.position,0.5f);
            Debug.Log("playing footstep sound");
        }
    }
}
