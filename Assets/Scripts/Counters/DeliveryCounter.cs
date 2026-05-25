using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter  : BaseCounter
{
    public static DeliveryCounter instance{get; private set;}

    private void Awake()
    {
        instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() &&
            player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            //player有kitchen object 并且是 plate
            DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
           
        }
    }
}
