using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounter : BaseCounter,IHasProgress
{
    [FormerlySerializedAs("cutKitchenObjectArray")]
    [FormerlySerializedAs("cutKitchenObject")] 
    [SerializeField] private CuttingRecipeSO[] cutKitchenObjectSOArray;

    public event EventHandler<IHasProgress.OnProgressChangeArgs> onProgressChange;
    public event EventHandler onCut;
    private int cuttingProgress;
    
    public override void Interact(Player player)
    {
        cuttingProgress = 0;
        if (!HasKitchenObject())
        {
            //there is no kitchen object here
            if (player.HasKitchenObject())
            {
                if (!GetOutputForInput(player.GetKitchenObject().getKitchenObjectSO()))
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
                onProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeArgs()
                {
                    progressNormalized = 1f
                });
            }
        }
    }

    public override void InteractAlternate(Player player)
    {

        //check is player has a kitchen object &&
        //check is there any recipe for input
        if (HasKitchenObject() && HasRecipeForInput(GetKitchenObject().getKitchenObjectSO()))
        {
            CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeForInput(GetKitchenObject().getKitchenObjectSO());
            cuttingProgress++;
            onCut?.Invoke(this,EventArgs.Empty);
            
            onProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeArgs()
            {
                progressNormalized = (float)cuttingProgress/(cuttingRecipeSo.maxCuttingProgress)
            });
            
            
            if (cuttingProgress >= cuttingRecipeSo.maxCuttingProgress)
            {
                //save a cutting kitchen obj for the kitchen obj which be put on the cutting counter 
                KitchObjectSO cuttingKitchenObjectSO = GetOutputForInput(GetKitchenObject().getKitchenObjectSO());
                //destroy the original kitchen object
                GetKitchenObject().DestroySelf();
                //and create a new one but sliced

                KitchenObject.CreateKitchenObject(cuttingKitchenObjectSO, this);
            }
        }
        else
        {
            //there is no recipe for the input
            Debug.Log("dont have recipe");
            return;
        }
           
    }
    
    private KitchObjectSO GetOutputForInput(KitchObjectSO inputKitchenObjectSO)
    {
        if (GetCuttingRecipeForInput(inputKitchenObjectSO) == null)
        {
            return null;
        }
        CuttingRecipeSO cuttingRecipe = GetCuttingRecipeForInput(inputKitchenObjectSO);
        return cuttingRecipe.output;
    }

    private bool HasRecipeForInput(KitchObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipe= GetCuttingRecipeForInput(inputKitchenObjectSO);
        if (cuttingRecipe != null)
        {
            return true;
        }
        return false;
    }

    private CuttingRecipeSO GetCuttingRecipeForInput(KitchObjectSO inputKitchenObjectSO)
    {
        foreach (var c in cutKitchenObjectSOArray)
        {
            if (inputKitchenObjectSO!=null&& inputKitchenObjectSO == c.input)
            {
                return c;
            }
        }
        return null;
    }
}
