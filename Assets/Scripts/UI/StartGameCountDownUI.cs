using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGameCountDownUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI countdownText;
    private Animator countdownAnimator;
    private int previousCountDownNum;
    private const string NUM_POP_UP="NumPopUp";
    private void Awake()
    {
       countdownAnimator= GetComponent<Animator>();
    }

    private void Start()
   {
      KitchenGameManager.instance.OnStateChanged += KitchenGameManager_OnStateChanged;
      Hide();
   }

   private void Update()
   {
      int countDownNum=Mathf.CeilToInt(KitchenGameManager.instance.GetCountdownTimer());
      
      countdownText.text = countDownNum.ToString();
      if (previousCountDownNum != countDownNum)
      {
         previousCountDownNum = countDownNum;
         countdownAnimator.SetTrigger(NUM_POP_UP);
         SoundManager.instance.PlayCountDownSound(Camera.main.transform.position,0.3f);
      }
   }

   private void KitchenGameManager_OnStateChanged(object sender, EventArgs e)
   {
      if (KitchenGameManager.instance.IsCountdownToStart())
      {
         Show();
      }
      else
      {
         Hide();
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
