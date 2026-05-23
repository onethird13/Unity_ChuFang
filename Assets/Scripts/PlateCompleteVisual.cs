using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
   [Serializable]
   public struct KitchenObject_GameObject
   {
      public KitchenObjectSO kitchenObjectSO;
      public GameObject gameObject;
   }
   [SerializeField] 
   private PlateKitchenObject plateKitchenObject;

   [SerializeField] 
   private List<KitchenObject_GameObject> kitchenObjectGameObjectList;
   private void Start()
   {
      plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
      foreach (var kitchenObjectGameObject in kitchenObjectGameObjectList)
      {
         kitchenObjectGameObject.gameObject.SetActive(false);
      }
   }

   private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
   {
      foreach (KitchenObject_GameObject kitchenObjectGameObject in kitchenObjectGameObjectList)
      {
         if (e.kitchenObjectSO == kitchenObjectGameObject.kitchenObjectSO)
         {
            kitchenObjectGameObject.gameObject.SetActive(true);
         }
      }
   }
}
