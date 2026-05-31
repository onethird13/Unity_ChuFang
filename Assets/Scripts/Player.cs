using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

/// <summary>
/// 玩家控制器：负责处理玩家移动、与柜台交互以及持有厨房物品。
/// </summary>
public class Player : NetworkBehaviour,IKitchenObjectParent
{
    public event EventHandler OnPickup; 
    
    
    public static event EventHandler OnAnyPlayerSpawned;
    /// <summary>
    /// 单例实例，方便全局访问玩家对象。
    /// </summary>
    public static Player LocalInstance{get;private set;}

    /// <summary>
    /// 玩家移动速度。
    /// </summary>
    [SerializeField] private float moveSpeed;
    /// <summary>
    /// 玩家旋转（朝向）速度。
    /// </summary>
    [SerializeField]private float rotateSpeed;
    /// <summary>
    /// 柜台所在的层掩码，用于射线检测。
    /// </summary>
    [SerializeField] private LayerMask counterLayerMask;
    /// <summary>
    /// 当前玩家持有的厨房物品。
    /// </summary>
    private KitchenObject kitchenObject;
    /// <summary>
    /// 标记玩家是否正在移动。
    /// </summary>
    private bool isWalking;
    /// <summary>
    /// 玩家最后一次进行交互尝试时的朝向方向。
    /// </summary>
    private Vector3 lastInteractDirection;
    /// <summary>
    /// 当前射线检测到的、被选中的柜台。
    /// </summary>
    private BaseCounter selectedCounter;
    /// <summary>
    /// 厨房物品在玩家身上的挂载点（Transform）。
    /// </summary>
    [SerializeField] private Transform kitchenObjectHoldPoint;
    /// <summary>
    /// 当选中的柜台发生变化时触发的事件。
    /// </summary>
    public event EventHandler<OnSelectedCounterChangedEventArgs> onSelectedCounterChanged;
    /// <summary>
    /// 选中柜台变化事件的事件参数类。
    /// </summary>
    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        /// <summary>
        /// 新选中的柜台。
        /// </summary>
        public BaseCounter selectedCounter;
    }

    /// <summary>
    /// 初始化单例实例，确保场景中只有一个 Player。
    /// </summary>
    private void Awake()
    {
       
        /*instance = this;*/
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
            OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// 初始化玩家速度和输入事件监听。
    /// </summary>
    private void Start()
    {
        moveSpeed = 7f;
        rotateSpeed = 7f;
        GameInput.instance.OnInteractAction += OnInteractAction;
        GameInput.instance.OnInteractAlternateAction += OnInteractAlternateAction;
    }

    public static void ResetStaticData()
    {
        OnAnyPlayerSpawned = null;
    }
    private void OnInteractAlternateAction(object sender,EventArgs args)
    {
        if (!KitchenGameManager.instance.IsGamePlaying())
        {
            return;
        }
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }

    }
    /// <summary>
    /// 交互事件回调：当玩家按下交互键时，与当前选中的柜台进行交互。
    /// </summary>
    private void OnInteractAction(object sender,System.EventArgs e)
    {
        if (!KitchenGameManager.instance.IsGamePlaying())
        {
            return;
        }
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }

    }

    /// <summary>
    /// 每帧更新：处理移动和交互检测。
    /// </summary>
    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        HandleMovement();
        HandleInteraction();
    }

    /// <summary>
    /// 返回玩家是否正在移动。
    /// </summary>
    public bool IsWalking()
    {
        return isWalking;
    }

    /// <summary>
    /// 处理玩家与柜台的交互检测，使用射线检测玩家朝向的柜台。
    /// </summary>
    private void HandleInteraction()
    {
        Vector2 inputVector = GameInput.instance.GetMovementVectorNormalized();

        Vector3 moveDirection=new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDirection != Vector3.zero)
        {
         lastInteractDirection = moveDirection;
        }
        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position+Vector3.up*0.5f,lastInteractDirection, out RaycastHit raycastHit,
                interactionDistance,counterLayerMask))
        {
            /*Debug.Log(raycastHit.collider.name);*/
            if (raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter basecounter))
            {
                //射线检测到
                /*clearCounter.Interact();*/
                if (basecounter != selectedCounter)
                {
                    selectedCounter = basecounter;
                    SetSelectedCounter(selectedCounter);
                }
            }
        }
        else
        {
            //射线没检测到
            selectedCounter = null;
            SetSelectedCounter(selectedCounter);
        }
        /*Debug.Log(selectedCounter);*/
    }

    /// <summary>
    /// 触发选中柜台变化事件，通知 UI 或其他监听者更新。
    /// </summary>
    private void SetSelectedCounter(BaseCounter clearCounter)
    {
        clearCounter = this.selectedCounter;
        onSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs()
        {
            selectedCounter = this.selectedCounter
        });
    }

    /// <summary>
    /// 处理玩家移动，包括碰撞检测和沿墙滑动逻辑。
    /// </summary>
    private void HandleMovement()
    {
        Vector2 inputVector = GameInput.instance.GetMovementVectorNormalized();

        Vector3 moveDirection=new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .3f;
        float playerHeight = 2f;
        // 使用胶囊体投射检测前方是否有障碍物
        bool canMove= !Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerRadius,
            moveDirection,moveDistance);
        if (!canMove)
        {
            //意味着无法移动，此时我们尝试分开向量
            //先试试能否在x轴上移动
            //发出一道射线，如果距离内没有检测到碰撞，返回false，!false就是可以移动，反之亦然
            canMove =(moveDirection.x!<=-0.5f || moveDirection.x>=0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 
                playerHeight, playerRadius, new Vector3(moveDirection.x,0,0), moveDistance);
            if (canMove)
            {
                //可以，那移动方向就是x
                moveDirection=new Vector3(moveDirection.x, 0, 0);
               
                /*Debug.Log("在x移动");*/
            }
            else
            {
                //无法在x上移动，试试z
                canMove=(moveDirection.z<=0.5f || moveDirection.z>=0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius,new Vector3(0, 0, moveDirection.z),moveDistance);
                if (canMove)
                {
                    //意味着可以在z移动，方向就是z
                    moveDirection=new Vector3(0, 0, moveDirection.z);
                   
                    /*Debug.Log("z can move");*/
                }
                else
                {
                    //意味着无法进行任何移动
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDirection*(Time.deltaTime*moveSpeed);
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime*rotateSpeed);
        isWalking = (moveDirection != Vector3.zero);
        
    }

    /// <summary>
    /// 设置当前持有的厨房物品。
    /// </summary>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (this.kitchenObject != null)
        {
            OnPickup?.Invoke(this,EventArgs.Empty);
        }
    }

    /// <summary>
    /// 获取当前持有的厨房物品。
    /// </summary>
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    /// <summary>
    /// 清空当前持有的厨房物品。
    /// </summary>
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    /// <summary>
    /// 返回玩家是否正持有厨房物品。
    /// </summary>
    public bool HasKitchenObject()
    {
        if (kitchenObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    /// <summary>
    /// 返回厨房物品应跟随的挂载点 Transform。
    /// </summary>
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
}
