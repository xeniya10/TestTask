using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseScreen : MonoBehaviour, IScreen
{
    [SerializeField] private Button _actionButton;

    public void SubscribeToButton(Action callBack)
    {
        _actionButton.onClick.AddListener(callBack.Invoke);
    }

    public void SetActive(bool isActivated)
    {
        gameObject.SetActive(isActivated);
    }
}
