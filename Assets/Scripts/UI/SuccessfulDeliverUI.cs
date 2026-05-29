using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessfulDeliverUI : MonoBehaviour
{
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField]private Image image;

    private float showTimer;
    private float showtimeMax;
    private bool isSrartToCountTime;

    private void Start()
    {
        
        deliveryCounter.OnDelivery += deliveryCounter_OnDelivery;
        image.gameObject.SetActive(false);
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
            isSrartToCountTime = false;
        }
    }

    private void deliveryCounter_OnDelivery(object sender, DeliveryCounter.OnDeliveryEventArgs e)
    {
        if (e.isSuccess)
        {
            image.gameObject.SetActive(true);
            isSrartToCountTime = true;
        }
        else
        {
            image.gameObject.SetActive(false);
            isSrartToCountTime = false;
        }
    }
}
