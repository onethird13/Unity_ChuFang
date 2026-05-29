using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
   public event EventHandler OnStateChanged;
   public event EventHandler OnGamePaused;
   public event EventHandler OnGameUnpaused;
   public static KitchenGameManager instance{get; private set;}   
   private enum GameState
   {
      WaitingToStart,
      CountdownToStart,
      Playing,
      GameOver
   }
   

   private GameState gameState;
   
   private float countdownTimer = 3f;
   private float playingTimerMax = 20f;
   private float playingTimer;
   private bool isGamePaused=false;
   

   private void Awake()
   {
      playingTimer = 0;
      instance = this;
      gameState=GameState.WaitingToStart;
   }

   private void Start()
   {
      GameInput.instance.OnPauseAction += GameInput_OnPauseAction;
      GameInput.instance.OnInteractAction += OnInteractAction_GameInput;
   }

   private void OnInteractAction_GameInput(object sender, EventArgs args)
   {
      gameState = GameState.CountdownToStart;
      OnStateChanged?.Invoke(this, EventArgs.Empty);
   }

   private void GameInput_OnPauseAction(object sender, EventArgs e)
   {
      TogglePauseGame();
   }

   private void Update()
   {
      switch (gameState)
      {
         case GameState.WaitingToStart:
            break;
         case GameState.CountdownToStart:
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
               gameState = GameState.Playing;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case GameState.Playing:
            playingTimer+=Time.deltaTime;
            if (playingTimer >= playingTimerMax)
            {
               gameState = GameState.GameOver;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case GameState.GameOver:
            break;
      }
    
   }
   public bool IsGamePlaying()
   {
      return gameState == GameState.Playing;
   }

   public bool IsCountdownToStart()
   {
      return gameState == GameState.CountdownToStart;
   }

   public float GetCountdownTimer()
   {
      return countdownTimer;
   }
   public bool IsGameOver()
   {
      return gameState == GameState.GameOver;
   }
   public float GetPlayingTimeNormalized()
   {
      return playingTimer/playingTimerMax;
   }

   public void TogglePauseGame()
   {
      if (isGamePaused)
      {
         Time.timeScale = 1f;
         isGamePaused=false;
         OnGameUnpaused?.Invoke(this, EventArgs.Empty);
      }
      else
      {
         Time.timeScale = 0f;
         isGamePaused=true;
         OnGamePaused?.Invoke(this, EventArgs.Empty);
      }
      
   }
   
}
