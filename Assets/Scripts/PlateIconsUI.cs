using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconsUI : MonoBehaviour
{
   [SerializeField] private PlateKitchenObject plateKitchenObject;
   [SerializeField] private Transform iconTemplate;

   private void Start()
   {
      iconTemplate.gameObject.SetActive(false);
      plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
   }

   private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs args)
   {
      UpdateVisual();
   }

   private void UpdateVisual()
   {
      List<KitchenObjectSO> IngredientList = plateKitchenObject.GetIngredientList();
      //删除所有元素
      foreach (Transform child in transform)
      {
         if (child == iconTemplate)
         {
            continue;
         }
         Destroy(child.gameObject);
      }
      //查找元素，有则显示
      foreach (var kitchenObjectSO in IngredientList )
      {
            Transform iconTransform= Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetIconAskitchenObjectSO(kitchenObjectSO);
      }
     
   }
}
