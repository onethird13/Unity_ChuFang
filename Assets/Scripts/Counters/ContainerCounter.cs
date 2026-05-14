using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
   [SerializeField] private KitchObjectSO kitchenObjectSO;
   
   public event EventHandler OnPLayerGrabobject;
  
   //当玩家与其互动，玩家手上需要多一个KitchenObject
   public override void  Interact(Player player)
   {
      if (player.HasKitchenObject())
      {
         Debug.Log(player.name +"already has a kitchenObject");
      }
      else
      {
         KitchenObject.CreateKitchenObject(kitchenObjectSO, player);
         OnPLayerGrabobject?.Invoke(this, EventArgs.Empty);
      }
   }

   
}
