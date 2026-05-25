using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
   public static SoundManager instance{get;private set;}
   [SerializeField]
   private AudioClipRefsSO clipRefsSO;

   private void Awake()
   {
      instance = this;
   }

   private void Start()
   {
      DeliveryManager.Instance.OnRecipeSuccessed += DeliveryManager_OnRecipeSuccessed;
      DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
      CuttingCounter.OnAnyCutting += CuttingCounter_OnAnyCutting;
      Player.instance.OnPickup += Player_OnPickup;
      BaseCounter.OnAnyObjectPlaceedHere += BaseCounter_OnAnyObjectPlacedHere;
      TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
   }

   public void PlayFootStepSound(Vector3 position,float volume)
   {
      Debug.Log($"Footstep clips: {clipRefsSO.footStep.Length}, pos: {position}, volume: {volume}");
      PlaySoundArray(clipRefsSO.footStep,position,volume);
   }

   private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs args)
   {
      TrashCounter trashCounter = sender as TrashCounter;
      PlaySoundArray(clipRefsSO.trash,trashCounter.transform.position,1f);
   }

   private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
   {
      BaseCounter baseCounter = sender as BaseCounter;
      PlaySoundArray(clipRefsSO.objectDrop,baseCounter.transform.position,1f);
   }
   private void Player_OnPickup(object sender, EventArgs args)
   {
      KitchenObject kitchenObject = Player.instance.GetKitchenObject();
      PlaySoundArray(clipRefsSO.objectPickup,kitchenObject.transform.position,1f);
   }

   private void CuttingCounter_OnAnyCutting(object sender, EventArgs e)
   {
      CuttingCounter cuttingCounter=sender as CuttingCounter;
      PlaySoundArray(clipRefsSO.chop,cuttingCounter.transform.position,1f);
   }

   private void DeliveryManager_OnRecipeSuccessed(object sender,EventArgs args)
   {
     DeliveryCounter deliveryCounter=DeliveryCounter.instance;
      PlaySoundArray(clipRefsSO.deliverySuccessed, deliveryCounter.transform.position, 1f);
   }

   private void DeliveryManager_OnRecipeFailed(object sender,EventArgs args)
   {
      DeliveryCounter deliveryCounter=DeliveryCounter.instance;
      PlaySoundArray(clipRefsSO.delieveryFailed, deliveryCounter.transform.position, 1f);
   }

   public void PlaySound(AudioClip clip,Vector3 position,float volume)
   {
      AudioSource.PlayClipAtPoint(clip, position,volume);
      
   }
   public void PlaySoundArray(AudioClip[] clipArray,Vector3 position,float volume)
   {
      AudioSource.PlayClipAtPoint(clipArray[UnityEngine.Random.Range(0,clipArray.Length)], position,volume);
   }
}
