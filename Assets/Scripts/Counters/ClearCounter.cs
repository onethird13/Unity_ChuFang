using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Serialization;


public class ClearCounter : BaseCounter,IKitchenObjectParent
{
    [SerializeField] protected KitchenObjectSO kitchenObjectSO;
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
                if (player.GetKitchenObject().TryGetPlate(out _))
                {
                    //player手上拿着plate,此时应把kitchen object销毁，在plate kitchen object 里的列表加上相应类型的SO
                    PlateKitchenObject plateKitchenObject=player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().getKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();    
                    }
                }
                else
                {
                    //player拿着不是plate，判断clear counter上是否有盘子，如果有则把player.kitchen object放到盘子里
                    if (this.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //clear counter 上面是plate
                       //应该把东西存入盘子的列表并销毁对象
                       plateKitchenObject.TryAddIngredient(player.GetKitchenObject().getKitchenObjectSO());
                      player.GetKitchenObject().DestroySelf();
                    }
                    
                }
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
