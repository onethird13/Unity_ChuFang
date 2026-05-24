using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter  : BaseCounter
{
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
