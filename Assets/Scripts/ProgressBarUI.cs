using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]
    private Image barImage; 
        
    [SerializeField]
    private CuttingCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.onProgressChange += onProgressCHange_UI;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void onProgressCHange_UI(object sender,CuttingCounter.OnProgressChangeArgs args)
    {
        
        barImage.fillAmount = args.progressNormalized;
        if (args.progressNormalized == 0f || args.progressNormalized >= 1f)
        {
            Hide();
        }
        else
        {
            Show();
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
