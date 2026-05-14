using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
   
    [SerializeField] private FryingRecipeSO[]  fryingRecipeArray;
    private float maxFryingTime;
    public override void Interact(Player player)
    {
        //如果玩家手里有东西 且自己身上没东西 且这个东西在配方的输入里
        if (player.HasKitchenObject() && !HasKitchenObject() &&
           IsInputObjectInRecipes(player.GetKitchenObject().getKitchenObjectSO()))
        {
            //把东西放自己身上
            player.GetKitchenObject().SetKitchenObjectParent(this);
           
            if (GetFryingRecipeForInput(GetKitchenObject().getKitchenObjectSO()) == null)
            {
                return;
            }
            maxFryingTime = GetFryingRecipeForInput(GetKitchenObject().getKitchenObjectSO()).maxFryingTime;
            
        }
        
    }

    private void Update()
    {
        if (this.HasKitchenObject() && IsInputObjectInRecipes(this.GetKitchenObject().getKitchenObjectSO()))
        {
            maxFryingTime-=Time.deltaTime;
            if (maxFryingTime <= 0)
            {
                KitchenObject tempKitchenObject = this.GetKitchenObject();
                this.GetKitchenObject().DestroySelf();
                Debug.Log(GetOutputForInput(tempKitchenObject.getKitchenObjectSO()).objectName);
                KitchenObject.CreateKitchenObject(GetOutputForInput(tempKitchenObject.getKitchenObjectSO()),
                    this);
            }
        }
        
        
    }

    //判断输入是否在配方里
    private bool IsInputObjectInRecipes(KitchObjectSO kitchenObjectSO)
    {
        foreach (var f in fryingRecipeArray)
        {
            if (f.input == kitchenObjectSO)
            {
                return true;
            }
        }        
        return false;
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
        FryingRecipeSO cuttingRecipe= GetFryingRecipeForInput(inputKitchenObjectSO);
        if (cuttingRecipe != null)
        {
            return true;
        }
        return false;
    }
    

    private FryingRecipeSO GetFryingRecipeForInput(KitchObjectSO inputKitchenObjectSO)
    {
        foreach (var f in fryingRecipeArray )
        {
            if (inputKitchenObjectSO == f.input)
            {
                return f;
            }
        }
        return null;
    }
}

