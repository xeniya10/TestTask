using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCalculator
{
    private Camera _mainCamera;
    private Vector3 _offset;

    public PointCalculator()
    {
        _mainCamera = Camera.main;
    }

    public void CalculateOffset(Vector3 objectPosition, Vector2 eventPosition)
    {
        _offset = objectPosition - GetWoldPoint(_mainCamera, eventPosition, objectPosition.z);
    }

    public Vector3 GetPoint(Vector3 objectPosition, Vector2 eventPosition)
    {
        return GetWoldPoint(_mainCamera, eventPosition, objectPosition.z) + _offset;
    }

    private Vector3 GetWoldPoint(Camera camera, Vector3 eventPosition, float zPosition)
    {
        var plane = new Plane(Vector3.forward, new Vector3(0, 0, zPosition));
        var ray = camera.ScreenPointToRay(eventPosition);
        plane.Raycast(ray, out float enterDistance);
        return ray.GetPoint(enterDistance);
    }
}
