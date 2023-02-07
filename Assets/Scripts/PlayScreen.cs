using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayScreen : BaseScreen, IPointerDownHandler, IPointerUpHandler
{
    [NonSerialized] public bool SwipeIsActivated;
    public Action<float> SwipeEvent;
    
    private Vector2 _startPointerPosition;
    private Vector2 _endPointerPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SwipeIsActivated)
        {
            _startPointerPosition = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (SwipeIsActivated)
        {
            _endPointerPosition = eventData.position;
            var swipeLength = _endPointerPosition.x - _startPointerPosition.x;
            SwipeEvent.Invoke(swipeLength);
        }
    }
}
