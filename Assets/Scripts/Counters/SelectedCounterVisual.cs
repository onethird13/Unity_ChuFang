using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [FormerlySerializedAs("clearCounter")] [SerializeField] private BaseCounter baseCounter;
    
    [FormerlySerializedAs("selectedClearCounterVisual")] [FormerlySerializedAs("clearCounterVisual")]
    [SerializeField] private GameObject[] selectedClearCounterVisualArray;
    
    private void Start()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.onSelectedCounterChanged += Player_OnSeclectedCounterChanged;
        }
        else
        {
            Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
        }
    }

    private void Player_OnAnyPlayerSpawned(object sender, EventArgs e)
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.onSelectedCounterChanged -= Player_OnSeclectedCounterChanged;
            Player.LocalInstance.onSelectedCounterChanged += Player_OnSeclectedCounterChanged;
            
        }
    }

    private void Player_OnSeclectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs args)
    {
        if (args.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var s in selectedClearCounterVisualArray)
        {
            s.SetActive(true);
        }
       
    }

    private void Hide()
    {
        foreach (var s in selectedClearCounterVisualArray)
        {
            s.SetActive(false);
        }
    
    }
}

