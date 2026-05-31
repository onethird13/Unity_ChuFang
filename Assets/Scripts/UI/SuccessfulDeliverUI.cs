using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessfulDeliverUI : MonoBehaviour
{
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField]private Image image;
    [SerializeField]private Image imageError;

    private float showTimer;
    private float showtimeMax;
    private bool isSrartToCountTime;

    private void Start()
    {
        
        DeliveryManager.Instance.OnRecipeSuccessed+=DeliveryManager_OnRecipeSuccessed;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        image.gameObject.SetActive(false);
        imageError.gameObject.SetActive(false);
        showtimeMax = 2f;
        showTimer = showtimeMax;
    }

    private void Update()
    {
        if (isSrartToCountTime)
        {
            showTimer -= Time.deltaTime;
        }

        if (showTimer <= 0)
        {
            showTimer = showtimeMax;
            image.gameObject.SetActive(false);
            imageError.gameObject.SetActive(false);
            isSrartToCountTime = false;
        }
    }

    private void DeliveryManager_OnRecipeSuccessed(object sender, EventArgs e)
    {
        
            image.gameObject.SetActive(true);
            isSrartToCountTime = true;
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        imageError.gameObject.SetActive(true);
        isSrartToCountTime=true;
    }
}
