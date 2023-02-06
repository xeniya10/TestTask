using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : BaseObject, IDragHandler, IPointerDownHandler
{
    [SerializeField] private int _maxDistance;
    [SerializeField] private int _minDistance;
    [SerializeField] private int _forwardSpeed;
    [SerializeField] private float _directionalSpeed;
    [SerializeField] private float _swipeDuration;
    [SerializeField] private float _swipeSteps;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    
    private Camera _mainCamera;
    private float _zPosition;
    private Vector3 _offset;

    [NonSerialized] public ControlType SelectedType = ControlType.Swipe;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _zPosition = transform.position.z;
    }
    
    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * _forwardSpeed);
        
        if (SelectedType==ControlType.Arrow)
        {
            var inputX = Input.GetAxis("Horizontal");
            _startPosition = transform.localPosition;
            _endPosition = new Vector3(Mathf.Clamp(_startPosition.x + inputX, _minDistance, _maxDistance),
                _startPosition.y, _startPosition.z);
            transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, _directionalSpeed * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SelectedType == ControlType.Drag)
        {
            _offset = transform.position - GetWoldPoint(_mainCamera,eventData.position, _zPosition);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (SelectedType == ControlType.Drag)
        {
            _startPosition = transform.localPosition;
            var xEndPosition = _startPosition.x -
                (GetWoldPoint(_mainCamera, eventData.position, _zPosition) + _offset).normalized.x;
            _endPosition = new Vector3(Mathf.Clamp(xEndPosition, _minDistance, _maxDistance), _startPosition.y, _startPosition.z);
            transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, _directionalSpeed * Time.deltaTime);
        }
    }

    public void OnSwipe(float swipeLength)
    {
        if (SelectedType == ControlType.Swipe)
        {
            var swipe = _swipeSteps;
            if (swipeLength < 0)
            {
                swipe*=-1;
            }
            StartCoroutine(Lerp(swipe));
        }
    }
    
    IEnumerator Lerp(float swipeStep)
    {
        float currentTime = 0;
        _startPosition = transform.localPosition;
        var xEndPosition = Mathf.Clamp(_startPosition.x + swipeStep, _minDistance, _maxDistance);
        _endPosition = new Vector3(xEndPosition, _startPosition.y, _startPosition.z);

        while (currentTime < _swipeDuration)
        {
            currentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_startPosition,_endPosition,Mathf.SmoothStep(0,1, currentTime/_swipeDuration));
            yield return null;
        }
    }
    
    private Vector3 GetWoldPoint(Camera camera, Vector3 screenPosition, float zPosition)
    {
        var plane = new Plane(Vector3.forward, new Vector3(0, 0, zPosition));
        var ray = camera.ScreenPointToRay(screenPosition);
        plane.Raycast(ray, out float enterDistance);
        return ray.GetPoint(enterDistance);
    }
}
