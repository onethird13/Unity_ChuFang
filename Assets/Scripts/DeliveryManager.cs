using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;



public class DeliveryManager : NetworkBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccessed;
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO recipeSOList;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnTimer;
    private float spawnTimerMax;
    private float recipeDeliveredAmount;



    private void Awake()
    {
        recipeDeliveredAmount = 0;
        Instance = this;
        spawnTimerMax = 3f;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        if (!IsServer)
        {
            return;
        }

        if (waitingRecipeSOList.Count < 4)
        {
            spawnTimer += Time.deltaTime;
        }

        if (spawnTimer >= spawnTimerMax && KitchenGameManager.instance.IsGamePlaying())
        {
            spawnTimer = 0f;
           int watingRecipeSOIndex = UnityEngine.Random.Range(0, recipeSOList.RecipeSOList.Count);
           
            ShowNewWaitingRecipeClientRpc(watingRecipeSOIndex);
            /*waitingRecipeSOList.Add(waitingRecipeSO);
            RecipeSO waitingRecipeSO = recipeSOList.RecipeSOList[watingRecipeSOIndex];
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);*/
            /*Debug.Log(waitingRecipeSO);*/
        }
    }
    [ClientRpc] 
    private void ShowNewWaitingRecipeClientRpc(int recipeListIndex)
    {
        waitingRecipeSOList.Add(recipeSOList.RecipeSOList[recipeListIndex]);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        int i = 0;
        foreach (RecipeSO waitingRecipeSO in waitingRecipeSOList)
        {
           
            //遍历每个正在等待的食材
            bool waitingRecipeContentMatched = false;   
            if (waitingRecipeSO.KitchenObjectSOList.Count == plateKitchenObject.GetIngredientList().Count)
            {
                //数量相同
              
                foreach (KitchenObjectSO kitchenObjectSO in waitingRecipeSO.KitchenObjectSOList)
                {
                    //遍历每个等待食材的kitchen object
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO ingredient in plateKitchenObject.GetIngredientList())
                    {
                        //遍历每个plate里面的kitchen object，让等待食材的ko去匹配plate的 ko
                        if (kitchenObjectSO == ingredient)
                        {
                            //有这个食材 返回true，继续遍历下一个等待食材的ko
                            ingredientFound = true;
                            break;
                        }
                    }
                    //没有这个食材，返回false
                    

                    if (ingredientFound == true)
                    {
                        //匹配到ko，继续匹配下一个ko
                        waitingRecipeContentMatched = true;
                        continue;
                    }
                    else
                    {
                        //没匹配到，直接break开始下一轮
                        waitingRecipeContentMatched = false;
                        break;
                    }
                    
                }
              
               
            }
            else
            {
                //数量不同,直接开始下一个
                i++;
              continue;
            
            }
            if (waitingRecipeContentMatched == true)
            {
                //匹配到，销毁waiting list里的匹配到的目标，并销毁plate对象
                plateKitchenObject.DestroySelf();
                DeliverCorrectRecipeServerRpc(i);
                return;
            }
            else
            {
                //没匹配到，继续下一个waiting recipe 的匹配
                i++; 
                continue;
            }
        }
        //没匹配到
        DeliverIncorrectRecipeServerRpc();
    }
    [ServerRpc(RequireOwnership =  false)]
    private void DeliverIncorrectRecipeServerRpc()
    {
        DeliverIncorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc()
    {
        Debug.Log("没匹配到");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
    
    [ServerRpc(RequireOwnership =  false)]
    private void DeliverCorrectRecipeServerRpc(int waitingRecipeSOIndex)
    {
       DeliverCorrectRecipeClientRpc(waitingRecipeSOIndex);
      
    }

    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int  waitingRecipeSOIndex)
    {
        waitingRecipeSOList.RemoveAt(waitingRecipeSOIndex);
       // plateKitchenObject.DestroySelf();
        Debug.Log("玩家传入了正确的食物");
        OnRecipeSuccessed?.Invoke(this, EventArgs.Empty);
        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        recipeDeliveredAmount++;
        
    }
    

    public List<RecipeSO> GetWatingRecipe()
    {
        return  waitingRecipeSOList;
    }

    public float GetRecipeDeliveredAmount()
    {
        return recipeDeliveredAmount;
    }
}
