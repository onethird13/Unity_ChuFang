using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlateCounter plateCounter;
    private List<Transform> platesList = new List<Transform>();
    
    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender,EventArgs args)
    {
        Transform plateTransform=platesList[platesList.Count - 1];
        platesList.RemoveAt(platesList.Count - 1);
        Destroy(plateTransform.gameObject);
        
        
    }
    
    
    private void PlateCounter_OnPlateSpawned(object sender, EventArgs args)
    {
        float plateOffsetY = 0.1f;
       
     Transform plateVisualTransform= Instantiate(plateVisualPrefab, counterTopPoint);
     plateVisualTransform.transform.localPosition = new Vector3(0, plateOffsetY * platesList.Count, 0);
     platesList.Add(plateVisualTransform);

    }
}
