using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField]
    private FryingRecipeSO[] fryingRecipeArray;

    private float fryingTimer;
    private FryingRecipeSO fryingRecipe;

    private void Update()
    {
        if (HasKitchenObject() && 
            GetOutputForInput(GetKitchenObject().getKitchenObjectSO())!=null)
        {
            Debug.Log(GetOutputForInput(GetKitchenObject().getKitchenObjectSO()));
            fryingTimer += Time.deltaTime;
            FryingRecipeSO fryingRecipeSO = GetFryingRecipeForInput(GetKitchenObject().getKitchenObjectSO());
            Debug.Log("frying");
            if (fryingTimer > fryingRecipeSO.maxFryingTime)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.CreateKitchenObject(fryingRecipeSO.output,this);
                fryingTimer = 0f;
                Debug.Log("fried");
            }
        }
    }

    public override void Interact(Player player)
    {
       
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            if (player.HasKitchenObject())
            {
                if (GetOutputForInput(player.GetKitchenObject().getKitchenObjectSO())==null)
                {
                    return;
                }
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
    
    
    
    
    
    private KitchObjectSO GetOutputForInput(KitchObjectSO inputKitchenObjectSO)
    {
        if (GetFryingRecipeForInput(inputKitchenObjectSO) == null)
        {
            return null;
        }
        FryingRecipeSO cuttingRecipe = GetFryingRecipeForInput(inputKitchenObjectSO);
        return cuttingRecipe.output;
    }

    private bool HasRecipeForInput(KitchObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipe= GetFryingRecipeForInput(inputKitchenObjectSO);
        if (fryingRecipe != null)
        {
            return true;
        }
        return false;
    }

    private FryingRecipeSO GetFryingRecipeForInput(KitchObjectSO inputKitchenObjectSO)
    {
        foreach (var f in fryingRecipeArray)
        {
            if (inputKitchenObjectSO == f.input)
            {
                return f;
            }
        }
        return null;
    }
}
