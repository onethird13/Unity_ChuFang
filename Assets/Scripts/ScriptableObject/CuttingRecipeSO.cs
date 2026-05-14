using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchObjectSO input;
    public KitchObjectSO output;
    public int maxCuttingProgress;
}
