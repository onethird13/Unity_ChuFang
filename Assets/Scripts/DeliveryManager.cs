using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance{get; private set;}
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccessed;
    public event EventHandler OnRecipeFailed; 
    
    [SerializeField]
    private RecipeListSO recipeSOList;
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
        if ( waitingRecipeSOList.Count<4)
        {
            spawnTimer += Time.deltaTime;
        }
      
        if (spawnTimer >= spawnTimerMax)
        {
            spawnTimer = 0f;
            RecipeSO waitingRecipeSO = recipeSOList.RecipeSOList[UnityEngine.Random.Range(0, recipeSOList.RecipeSOList.Count)];
            waitingRecipeSOList.Add(waitingRecipeSO);
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            /*Debug.Log(waitingRecipeSO);*/
        }
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
                waitingRecipeSOList.RemoveAt(i);
                plateKitchenObject.DestroySelf();
                Debug.Log("玩家传入了正确的食物");
                OnRecipeSuccessed?.Invoke(this, EventArgs.Empty);
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                recipeDeliveredAmount++;
                return;
            }
            else
            {
                //没匹配到，继续下一个waiting recipe 的匹配
                i++;
                continue;
            }
           
        }
        Debug.Log("没匹配到");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        
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
