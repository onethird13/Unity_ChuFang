using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField]
    private FryingRecipeSO[] fryingRecipeArray;
    [SerializeField]
    private BurningRecipeSO[] burningRecipeArray;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;
    private float burningTimer;
    
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                if (HasKitchenObject() && 
                    GetOutputForInput(GetKitchenObject().getKitchenObjectSO())!=null)
                {
                    fryingTimer += Time.deltaTime;
                 
                    if (fryingTimer > fryingRecipeSO.maxFryingTime)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject kitchenObject=  KitchenObject.CreateKitchenObject(fryingRecipeSO.output,this);
                        fryingRecipeSO = GetFryingRecipeForInput(kitchenObject.getKitchenObjectSO());
                     
                        state = State.Fried;
                        burningRecipeSO=GetBurningRecipeForInput(kitchenObject.getKitchenObjectSO());
                        burningTimer = 0f;
                        
                    }
                }
                break;
            case State.Fried:
                burningTimer += Time.deltaTime;
              
                if (burningTimer > burningRecipeSO.maxBurningTime)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject kitchenObject=  KitchenObject.CreateKitchenObject(burningRecipeSO.output,this);
                  
                    state = State.Burned;
                }
                break;
            case State.Burned:
                break;
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
                 fryingRecipeSO = GetFryingRecipeForInput(GetKitchenObject().getKitchenObjectSO());
                 state = State.Frying;
                 fryingTimer = 0f;
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
                state = State.Idle;
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
    
    
    private BurningRecipeSO GetBurningRecipeForInput(KitchObjectSO inputKitchenObjectSO)
    {
        foreach (var b in burningRecipeArray)
        {
            if (inputKitchenObjectSO == b.input)
            {
                return b;
            }
        }
        return null;
    }
}
