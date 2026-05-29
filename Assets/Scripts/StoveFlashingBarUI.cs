using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveFlashingBarUI : MonoBehaviour
{
    private string IS_FLASHING="IsFlashing";
    [SerializeField]private StoveCounter stoveCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.onProgressChange += stoveCounter_onProgressChange;
        animator.SetBool(IS_FLASHING,false);
    }

    private void stoveCounter_onProgressChange(object sender, IHasProgress.OnProgressChangeArgs args)
    {
        float burnShowProgressAmount = 0.5f;
        bool show=stoveCounter.IsFried() && args.progressNormalized>=burnShowProgressAmount;
        animator.SetBool(IS_FLASHING,show);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

