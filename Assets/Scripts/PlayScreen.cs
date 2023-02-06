using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayScreen : BaseScreen, IPointerDownHandler, IPointerUpHandler
{
    public bool SwipeIsActive;
    public Action<float> SwipeEvent;
    
    private Vector2 _startPointerPosition;
    private Vector2 _endPointerPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SwipeIsActive)
        {
            _startPointerPosition = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (SwipeIsActive)
        {
            _endPointerPosition = eventData.position;
            var swipeLength = _endPointerPosition.x - _startPointerPosition.x;
            SwipeEvent.Invoke(swipeLength);
        }
    }
}
