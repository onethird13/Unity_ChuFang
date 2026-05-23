using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject:KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    //盘子上的物体实际上是一个列表,销毁应在其他地方做
    private List<KitchenObjectSO> ingredientList;
    [SerializeField] private List<KitchenObjectSO> validIngredients;

    private void Start()
    {
        ingredientList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validIngredients.Contains(kitchenObjectSO))
        {
            //这个食材不被允许
            return false;
        }
        if (ingredientList.Contains(kitchenObjectSO))
        {
            return false;
        }
        ingredientList.Add(kitchenObjectSO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs()
        {
            kitchenObjectSO = kitchenObjectSO
        });
        return true;
    }
    public List<KitchenObjectSO> GetIngredientList()
    {
        return ingredientList;
    }
}
