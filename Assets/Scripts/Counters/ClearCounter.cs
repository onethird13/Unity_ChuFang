using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Serialization;


public class ClearCounter : BaseCounter,IKitchenObjectParent
{
    [SerializeField] protected KitchObjectSO kitchenObjectSO;
     public override void  Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            if (player.HasKitchenObject())
            {
                //player has a kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            //there has a kitchen object
            if (player.HasKitchenObject())
            {
                //player has a kitchen object
            }
            else
            {
                //player dont have a kitchen object
                //we need to give this kitchen object to player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            
        }
    }
     
   
    
    
}
