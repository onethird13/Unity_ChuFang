using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
   [SerializeField] private GameObject stoveOnGameObject;
   [SerializeField]private GameObject particleGameObject;
   [SerializeField] private StoveCounter stoveCounter;

   private void Start()
   {
       stoveCounter.OnStateChanged += StoveCounter_OnSelectedCounterChanged;
   }

   private void StoveCounter_OnSelectedCounterChanged(object sender,
     StoveCounter.OnStateChangedEventArgs args)
   {
       bool isShow = args.state == StoveCounter.State.Frying || args.state == StoveCounter.State.Fried;
       stoveOnGameObject.SetActive(isShow);
       particleGameObject.SetActive(isShow);
   }
}
