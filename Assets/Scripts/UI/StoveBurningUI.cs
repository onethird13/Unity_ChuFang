using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurningUI : MonoBehaviour
{
    [SerializeField]private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.onProgressChange += stoveCounter_onProgressChange;
        Hide();
    }

    private void stoveCounter_onProgressChange(object sender, IHasProgress.OnProgressChangeArgs args)
    {
        float burnShowProgressAmount = 0.5f;
        bool show=stoveCounter.IsFried() && args.progressNormalized>=burnShowProgressAmount;
        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
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
