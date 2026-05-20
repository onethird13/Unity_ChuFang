using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO: ScriptableObject
{
   public KitchObjectSO input;
   public KitchObjectSO output;
   public float maxBurningTime;
}
