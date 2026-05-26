using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
   public event EventHandler OnStateChanged;
   public static KitchenGameManager instance{get; private set;}   
   private enum GameState
   {
      WaitingToStart,
      CountdownToStart,
      Playing,
      GameOver
   }

   private GameState gameState;
   private float waitingToStartTimer = 1f;
   private float countdownTimer = 3f;
   private float playingTimer = 20f;

   private void Awake()
   {
      instance = this;
      gameState=GameState.WaitingToStart;
   }

   private void Update()
   {
      switch (gameState)
      {
         case GameState.WaitingToStart:
            waitingToStartTimer -= Time.deltaTime;
            if (waitingToStartTimer <= 0)
            {
               gameState = GameState.CountdownToStart;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
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
            playingTimer-=Time.deltaTime;
            if (playingTimer <= 0)
            {
               gameState = GameState.GameOver;
               OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
            break;
         case GameState.GameOver:
            break;
      }
      Debug.Log(gameState);
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
}
