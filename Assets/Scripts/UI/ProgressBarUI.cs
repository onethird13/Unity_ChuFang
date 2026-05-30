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
    private GameObject hasProgressObject;
    private IHasProgress hasProgress;
    private void Start()
    {
        hasProgress = hasProgressObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError(hasProgressObject.name + " has no IHasProgress(Interfance)");
        }
        hasProgress.onProgressChange += onProgressChange_UI;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void onProgressChange_UI(object sender,IHasProgress.OnProgressChangeArgs args)
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
