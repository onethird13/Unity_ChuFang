using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
   
    public static OptionUI instance{get; private set;}
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI soundVolumeText;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    /*----------------game pad-------------------*/
    [SerializeField] private Button interactGamePadButton;
    [SerializeField] private Button interactAlternateGamePadButton;
    [SerializeField] private Button pauseGamePadButton;
    
    
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    /*----------------game pad-------------------*/
    [SerializeField] private TextMeshProUGUI interactGamePadText;
    [SerializeField] private TextMeshProUGUI interactAlternateGamePadText;
    [SerializeField] private TextMeshProUGUI pauseGamePadText;

    [SerializeField] private GameObject pressToRebindAKeyGameObject;
    private Action onCloseButtonLoadAction;
    private void Awake()
    {
        instance = this;
        musicButton.onClick.AddListener((() =>
        {
           MusicManager.instance.ChangeVolume(.1f);
           UpdateVisual();
        }));
        
        soundButton.onClick.AddListener((() =>
        {
            SoundManager.instance.ChangeVolume(0.1f);
            UpdateVisual();
        }));
        closeButton.onClick.AddListener((() =>
        {
            Hide();
            onCloseButtonLoadAction();
        }));
        moveUpButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
    
        }));

        moveDownButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        }));

        moveLeftButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        }));

        moveRightButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        }));

        interactButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        }));

        interactAlternateButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.InteractAlternate);
        }));

        pauseButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        }));
        interactGamePadButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.GamePad_Interact);
        }));

        interactAlternateGamePadButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.GamePad_InteractAlternate);
        }));

        pauseGamePadButton.onClick.AddListener((() =>
        {
            RebindBinding(GameInput.Binding.GamePad_Pause);
        }));
        
        
    }

    private void Start()
    {
        KitchenGameManager.instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
        HidePressToRebindAKeyGameObject();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs args)
    {
        Hide();
    }
    public void Show(Action onCloseButtonLoadAction)
    {
        this.onCloseButtonLoadAction=onCloseButtonLoadAction;
        gameObject.SetActive(true);
        soundButton.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateVisual()
    {
        soundVolumeText.text ="SoundVolume:"+Mathf.Ceil(SoundManager.instance.GetVolume()*10).ToString();
        musicVolumeText.text = "MusicVolume" + Mathf.Ceil(MusicManager.instance.GetVolume()*10).ToString();
        
        moveUpText.text=GameInput.instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text=GameInput.instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text=GameInput.instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text=GameInput.instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text=GameInput.instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text=GameInput.instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text=GameInput.instance.GetBindingText(GameInput.Binding.Pause);
        interactGamePadText.text=GameInput.instance.GetBindingText(GameInput.Binding.GamePad_Interact);
        interactAlternateGamePadText.text=GameInput.instance.GetBindingText(GameInput.Binding.GamePad_InteractAlternate);
        pauseGamePadText.text=GameInput.instance.GetBindingText(GameInput.Binding.GamePad_Pause);
    }

    private void ShowPressToRebindAKeyGameObject()
    {
        pressToRebindAKeyGameObject.SetActive(true);
    }

    private void HidePressToRebindAKeyGameObject()
    {
        pressToRebindAKeyGameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindAKeyGameObject();
        GameInput.instance.ReBinding(binding, () =>
        {
            HidePressToRebindAKeyGameObject();
            UpdateVisual();
        });
    }
}
