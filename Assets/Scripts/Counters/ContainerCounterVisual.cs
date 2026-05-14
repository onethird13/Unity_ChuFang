using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter ContainerCounter;
    [SerializeField] private Animator ContainerCounterAnimator;
    private const string OPEN_CLOSE="OpenClose";
    private void Awake()
    {
        ContainerCounterAnimator=GetComponent<Animator>();
    }

    void Start()
    {
        ContainerCounter.OnPLayerGrabobject += ContainerCounter_OnPLayerGrabobject;
    }

    private void ContainerCounter_OnPLayerGrabobject(object sender,System.EventArgs args)
    {
        ContainerCounterAnimator.SetTrigger(OPEN_CLOSE);
    }

   
    
}
