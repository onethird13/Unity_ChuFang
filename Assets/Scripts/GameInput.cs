using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
   private PlayerInputAction playerInputAction;
   public event EventHandler OnInteractAction;
   public event EventHandler OnInteractAlternateAction;
   public event EventHandler OnPauseAction;
   
   public static GameInput instance{get; private set;}
   
   private void Awake()
   {
      instance = this;
      playerInputAction = new PlayerInputAction();
      playerInputAction.Enable();
      playerInputAction.Player.Interact.performed += InteractPerformed;
      playerInputAction.Player.InteractAlternate.performed += InteractAlternate_Performed;
      playerInputAction.Player.Pause.performed += Pause_Performed;
   }

   private void OnDestroy()
   {
      playerInputAction.Player.Interact.performed -=InteractPerformed;
      playerInputAction.Player.InteractAlternate.performed-= InteractAlternate_Performed;
      playerInputAction.Player.Pause.performed -= Pause_Performed;
      playerInputAction.Dispose();
   }

   public void Pause_Performed(InputAction.CallbackContext ctx)
   {
      OnPauseAction?.Invoke(this, EventArgs.Empty);
   }

   private void InteractAlternate_Performed(InputAction.CallbackContext ctx)
   {
      OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
   }

   private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
   {
      /*Debug.Log(ctx);*/
      OnInteractAction?.Invoke(this, EventArgs.Empty);
   }

   public Vector2 GetMovementVectorNormalized()
   {
      Vector2 inputVector=playerInputAction.Player.Move.ReadValue<Vector2>();
      inputVector.Normalize();
      return inputVector;
      
   }
}
