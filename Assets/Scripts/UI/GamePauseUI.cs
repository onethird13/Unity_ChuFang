using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
   [SerializeField] private Button mainMenuButton;
   [SerializeField] private Button resumeButton;

   private void Awake()
   {
      mainMenuButton.onClick.AddListener(() =>
      {
         Loader.LoadScene(Loader.Scene.MainMenuScene);
      });
      resumeButton.onClick.AddListener((() =>
      {
        
         KitchenGameManager.instance.TogglePauseGame();
      }));
   }

   private void Start()
   {
      KitchenGameManager.instance.OnGamePaused += KitchenGameManager_OnGamePaused;
      KitchenGameManager.instance.OnGameUnpaused+=KitchenGameManager_OnGameUnPaused;
      Hide();
   }

   private void KitchenGameManager_OnGameUnPaused(object sender,EventArgs args)
   {
      Hide();
   }

   private void KitchenGameManager_OnGamePaused(object sender,EventArgs args)
   {
      Show();
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
