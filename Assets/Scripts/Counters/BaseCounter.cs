using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlaceedHere;
    [SerializeField] private Transform counterTopPoint;
    
    private KitchenObject kitchenObject;
    public static void ResetStaticData()
    {
        OnAnyObjectPlaceedHere=null;   
    }
    public virtual void Interact(Player player)
    {
        Debug.Log("BaseCounter Interact");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.Log("BaseCounter InteractAlternate");
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyObjectPlaceedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
}
