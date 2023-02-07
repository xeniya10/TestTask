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

    private PointCalculator _calculator;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    [NonSerialized] public ControlType SelectedType = ControlType.Swipe;
    
    private void Start()
    {
        _calculator = new PointCalculator();
    }
    
    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * _forwardSpeed);
        
        if (SelectedType==ControlType.Arrow)
        {
            var inputX = Input.GetAxis("Horizontal");
            _startPosition = transform.position;
            _endPosition = new Vector3(Mathf.Clamp(_startPosition.x + inputX, _minDistance, _maxDistance),
                _startPosition.y, _startPosition.z);
            transform.position = Vector3.Lerp(_startPosition, _endPosition, _directionalSpeed * Time.deltaTime);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SelectedType == ControlType.Drag)
        {
            _calculator.CalculateOffset(transform.position, eventData.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (SelectedType == ControlType.Drag)
        {
            _startPosition = transform.position;
            _endPosition = new Vector3(Mathf.Clamp(
                _calculator.GetPoint(transform.position, eventData.position).x, _minDistance, _maxDistance), _startPosition.y, _startPosition.z);
            transform.position = Vector3.Lerp(_startPosition, _endPosition, _directionalSpeed * Time.deltaTime);
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
        _startPosition = transform.position;
        _endPosition = new Vector3(Mathf.Clamp(_startPosition.x + swipeStep, _minDistance, _maxDistance), _startPosition.y, _startPosition.z);

        while (currentTime < _swipeDuration)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(_startPosition,_endPosition,Mathf.SmoothStep(0,1, currentTime/_swipeDuration));
            yield return null;
        }
    }
}
