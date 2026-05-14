using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchObjectSO kitchenObjectSO;
   private IKitchenObjectParent kitchenObjectParent;
   public KitchObjectSO getKitchenObjectSO()
   {
      return kitchenObjectSO;
   }

   public  IKitchenObjectParent GetKitchenObjectParent()
   {
      return kitchenObjectParent;
   }

   public void SetKitchenObjectParent( IKitchenObjectParent kitchenObjectParent)
   {
      if (this.kitchenObjectParent != null)
      {
         this.kitchenObjectParent.ClearKitchenObject();
      }
      if ( kitchenObjectParent.HasKitchenObject())
      {
         Debug.Log( " has kitchenObject " +  kitchenObjectParent.GetKitchenObject().name);
      }
      this.kitchenObjectParent =  kitchenObjectParent;
      this.kitchenObjectParent.SetKitchenObject(this);
      transform.parent =  kitchenObjectParent.GetKitchenObjectFollowTransform();
      transform.localPosition = Vector3.zero;
      
   }

   public void DestroySelf()
   {
      kitchenObjectParent.ClearKitchenObject();
      Destroy(gameObject);
   }


   public static KitchenObject CreateKitchenObject(KitchObjectSO kitchenObjectSO,
      IKitchenObjectParent kitchenObjectParent)
   {
      Transform kitchenObjectTransform= Instantiate(kitchenObjectSO.prefab );
      KitchenObject kitchenObject = kitchenObjectTransform.transform.GetComponent<KitchenObject>();
      kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

      return kitchenObject;
   }
}
