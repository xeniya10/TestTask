using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseScreen : BaseObject
{
    [SerializeField] private Button _actionButton;

    public void SubscribeToButton(Action callBack)
    {
        _actionButton.onClick.AddListener(callBack.Invoke);
    }
}
