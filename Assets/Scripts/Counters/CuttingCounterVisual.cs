using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounterVisual : MonoBehaviour
{
    [FormerlySerializedAs("ContainerCounter")] 
    [SerializeField] private CuttingCounter cuttingCounter;
    
    private Animator CuttingConterAnimator;
    private const string CUT="Cut";
    private void Awake()
    {
        CuttingConterAnimator=GetComponent<Animator>();
    }
    void Start()
    {
        cuttingCounter.onCut += ContainerCounter_OnPLayerCut;
    }

    private void ContainerCounter_OnPLayerCut(object sender,System.EventArgs args)
    {
        CuttingConterAnimator.SetTrigger(CUT);
    }

   
    
}
