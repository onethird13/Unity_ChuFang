using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGameCountDownUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI countdownText;

   private void Start()
   {
      KitchenGameManager.instance.OnStateChanged += KitchenGameManager_OnStateChanged;
      Hide();
   }

   private void Update()
   {
      countdownText.text = Math.Ceiling(KitchenGameManager.instance.GetCountdownTimer()).ToString();
   }

   private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
   {
      if (KitchenGameManager.instance.IsCountdownToStart())
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
      gameObject.SetActive(true);
   }

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
