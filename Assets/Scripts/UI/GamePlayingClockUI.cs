using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image gamePlayingClockImage;
    
    
    private void Update()
    {
        gamePlayingClockImage.fillAmount = KitchenGameManager.instance.GetPlayingTimeNormalized();
        
    }
}
