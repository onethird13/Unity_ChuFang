using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{
    [SerializeField]private Button startHostButton;
    [SerializeField]private Button startClientButton;

    private void Start()
    {
        startHostButton.onClick.AddListener((() =>
        {
            Debug.Log("this:HOST");
            NetworkManager.Singleton.StartHost();
            Hide();
        }));
        startClientButton.onClick.AddListener((() =>
        {
            Debug.Log("Client");
            NetworkManager.Singleton.StartClient();
            Hide();
        }));
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
