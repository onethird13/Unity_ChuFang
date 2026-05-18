using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter
{
   
    [SerializeField] private FryingRecipeSO[]  fryingRecipeArray;
    public event EventHandler<FryingStatusChangedEventArgs> FryingStatusChanged;
    public class FryingStatusChangedEventArgs : EventArgs
    {
        public bool isFinshed;
    }

   
    private bool isFinished;

    private void Awake()
    {
        FryingStatusChanged += OnFryingStatusChanged;
    }

    private void OnFryingStatusChanged(object sender, EventArgs e)
    {
        if (!isFinished)
        {
            FinishFrying(GetKitchenObject().getKitchenObjectSO());
            isFinished = true;
            StartCoroutine(FryingTimer(GetFryingRecipeForInput(GetKitchenObject()
                .getKitchenObjectSO()).maxFryingTime));
        }
        else
        {
            Burned(GetKitchenObject().getKitchenObjectSO());
        }
    }

    IEnumerator FryingTimer(float time)
    {
        yield return new WaitForSeconds(time);
        FryingStatusChanged?.Invoke(this,new FryingStatusChangedEventArgs()
        {
            isFinshed = this.isFinished,
        } );
    }
    
    public override void Interact(Player player)
    {
        
       
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            if (player.HasKitchenObject())
            {
                //如果不在recipe里就直接return
                if (GetOutputForInput(player.GetKitchenObject().getKitchenObjectSO())==null)
                {
                    return;
                }
                //player has a kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
                isFinished = false;
                StartCoroutine(FryingTimer(GetFryingRecipeForInput(GetKitchenObject().
                    getKitchenObjectSO()).maxFryingTime));
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
                //player don't have a kitchen object
                //we need to give this kitchen object to player
                GetKitchenObject().SetKitchenObjectParent(player);
                isFinished = false;
                StopAllCoroutines();
            }
        }
        
        
    }
    
    //烤熟了
    private void FinishFrying(KitchObjectSO kitchenObjectSO)
    {
        KitchObjectSO tempKitchenObjectSO = GetOutputForInput(kitchenObjectSO) ;
        this.GetKitchenObject().DestroySelf();
        KitchenObject.CreateKitchenObject(tempKitchenObjectSO,this);
    }
    //烤糊了
    private void Burned(KitchObjectSO kitchenObjectSO)
    {
        Debug.Log("Burned");
        KitchObjectSO tempKitchenObjectSO = GetOutputForInput(kitchenObjectSO) ;
        this.GetKitchenObject().DestroySelf();
        Debug.Log("destroyed");
        KitchenObject.CreateKitchenObject(tempKitchenObjectSO, this);

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

