using System;
using UnityEngine;


public class PlateCounter : BaseCounter
{
    private float spawnPlateTimer;
    private float maxSpawnTime;
    [SerializeField] private KitchObjectSO plateSO;
    private void Start()
    {
        maxSpawnTime = 5f;
    }

    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= maxSpawnTime)
        {
            spawnPlateTimer = 0f;
        }
    }
}
