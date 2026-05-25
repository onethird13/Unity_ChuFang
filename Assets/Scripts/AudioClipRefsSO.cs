using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/AudioClipRefsSO")]
public class AudioClipRefsSO :ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] delieveryFailed;
    public AudioClip[] deliverySuccessed;
    public AudioClip[] footStep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;

}
