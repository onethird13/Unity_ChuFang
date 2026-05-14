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
 
   private void Awake()
   {
      playerInputAction = new PlayerInputAction();
      playerInputAction.Enable();
      playerInputAction.Player.Interact.performed += InteractPerformed;
      playerInputAction.Player.InteractAlternate.performed += InteractAlternate_Performed;
      
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
