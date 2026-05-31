using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
   [SerializeField]private TextMeshProUGUI moveUpText;
   [SerializeField]private TextMeshProUGUI moveDownText;
   [SerializeField]private TextMeshProUGUI moveLeftText;
   [SerializeField]private TextMeshProUGUI moveRightText;
   [SerializeField]private TextMeshProUGUI interactText;
   [SerializeField]private TextMeshProUGUI interactAlternateText;
   [SerializeField]private TextMeshProUGUI pauseText;

   private void Start()
   {
      UpdateVisual();
      GameInput.instance.OnRebinding += GameInput_OnRebinding;
      KitchenGameManager.instance.OnStateChanged += KitchenGameManager_OnStateChanged;
      Show();
   }

   private void KitchenGameManager_OnStateChanged(object sender, EventArgs args)
   {
      if (KitchenGameManager.instance.IsCountdownToStart() || 
          KitchenGameManager.instance.IsGamePlaying())
      {
         Hide();
      }
   }

   private void GameInput_OnRebinding(object sender, EventArgs args)
   {
      UpdateVisual();
   }


   private void UpdateVisual()
   {
      moveUpText.text=GameInput.instance.GetBindingText(GameInput.Binding.Move_Up);
      moveDownText.text=GameInput.instance.GetBindingText(GameInput.Binding.Move_Down);
      moveLeftText.text=GameInput.instance.GetBindingText(GameInput.Binding.Move_Left);
      moveRightText.text=GameInput.instance.GetBindingText(GameInput.Binding.Move_Right);
      interactText.text=GameInput.instance.GetBindingText(GameInput.Binding.Interact);
      interactAlternateText.text=GameInput.instance.GetBindingText(GameInput.Binding.InteractAlternate);
      pauseText.text=GameInput.instance.GetBindingText(GameInput.Binding.Pause);
      
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
