using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator:NetworkBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";
    [SerializeField]private Player player;


    private NetworkVariable<bool> isWalkingNetworkVariable = new
        NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        /*animator.SetBool(IS_WALKING, true);*/
    }

    private void Update()
    {
        if (IsOwner)
        {
            bool isWalking = player.IsWalking();
            if (isWalkingNetworkVariable.Value != isWalking)
            {
                isWalkingNetworkVariable.Value = isWalking;
            }
        }

        animator.SetBool(IS_WALKING,isWalkingNetworkVariable.Value);
    }
}
