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

   private const string PLAYER_PREFS_BINDINGS = "InputBindings";
   
   public static GameInput instance{get; private set;}
   public enum Binding
   {
      Move_Up,
      Move_Down,
      Move_Left,
      Move_Right,
      Interact,
      InteractAlternate,
      Pause,
   }
   private void Awake()
   {
      instance = this;
      playerInputAction = new PlayerInputAction();
      if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
      {
         playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
         Debug.Log("666");
      }
      playerInputAction.Enable();
      playerInputAction.Player.Interact.performed += InteractPerformed;
      playerInputAction.Player.InteractAlternate.performed += InteractAlternate_Performed;
      playerInputAction.Player.Pause.performed += Pause_Performed;
      Debug.Log(GetBindingText(Binding.Move_Up));
      
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

   public string GetBindingText(Binding binding)
   {
      switch (binding)
      {
         default:
         case Binding.Interact:
            return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
         case Binding.InteractAlternate:
            return playerInputAction.Player.InteractAlternate.bindings[0].ToDisplayString();
         case Binding.Pause:
            return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
         case Binding.Move_Up:
            return playerInputAction.Player.Move.bindings[1].ToDisplayString();
         case Binding.Move_Down:
            return playerInputAction.Player.Move.bindings[2].ToDisplayString();
         case Binding.Move_Left:
            return playerInputAction.Player.Move.bindings[3].ToDisplayString();
         case Binding.Move_Right:
            return playerInputAction.Player.Move.bindings[4].ToDisplayString();
      }
   }

   public void ReBinding(Binding binding,Action onActionReBound)
   {
      playerInputAction.Player.Disable();
      InputAction inputAction;
      int bindingIndex;
      switch (binding)
      {
         default:
         case Binding.Move_Up:
            inputAction = playerInputAction.Player.Move;
            bindingIndex = 1;
            break;
         case Binding.Move_Down:
            inputAction = playerInputAction.Player.Move;
            bindingIndex = 2;
            break;
         case Binding.Move_Left:
            inputAction = playerInputAction.Player.Move;
            bindingIndex = 3;
            break;
         case Binding.Move_Right:
            inputAction = playerInputAction.Player.Move;
            bindingIndex = 4;
            break;
         case Binding.Interact:
            inputAction = playerInputAction.Player.Interact;
            bindingIndex = 0;
            break;
         case Binding.InteractAlternate:
            inputAction = playerInputAction.Player.InteractAlternate;
            bindingIndex = 0;
            break;
         case Binding.Pause:
            inputAction = playerInputAction.Player.Pause;
            bindingIndex = 0;
            break;
         
      }
      inputAction.PerformInteractiveRebinding(bindingIndex).
         OnComplete(callback =>
      {
         callback.Dispose();
         playerInputAction.Enable();
         onActionReBound();
         PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,
            playerInputAction.SaveBindingOverridesAsJson());
         PlayerPrefs.Save();
         
      }).Start();
      
      
   }
}
