using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO: ScriptableObject
{
   public KitchObjectSO input;
   public KitchObjectSO output;
   public float maxFryingTime;
}
