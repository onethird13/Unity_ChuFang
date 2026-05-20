using System;
using UnityEngine;
using UnityEngine.Serialization;


public class PlateCounter : BaseCounter
{
    private float spawnPlateTimer;
    private float maxSpawnTime;
    [SerializeField] private KitchObjectSO plateSO;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [FormerlySerializedAs("plateAmount")] public float plateCount;
    public float maxPlateCount;
    
    
    
    
    
    
    private void Start()
    {
        maxSpawnTime = 2f;
        plateCount = 0;
        maxPlateCount = 4f;
    }

    private void Update()
    {
        if (plateCount >= maxPlateCount)
        {
            return;
        }
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= maxSpawnTime)
        {
            spawnPlateTimer = 0f;
            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            plateCount++;
        }
        
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            //玩家手里有东西
            if (this.HasKitchenObject())
            {
                //台子上有东西
                return;
            }
            else
            {
                //台子上没东西
                return;
            }
        }
        else
        {
            //玩家手里没东西
            if (plateCount>=0)
            {
                //台子上有东西
                KitchenObject.CreateKitchenObject(plateSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                plateCount--;
            }
            else
            {
                //台子上没东西
                Debug.Log("Plate not found");
            }
            
        }
    }
}
