using UnityEngine;
using VContainer;

public class CameraController : MonoBehaviour
{
    private Player _player;
    
    private Vector3 _startPosition;
    private Vector3 _offset;

    [Inject] public void Inject(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        _offset = transform.position - _player.transform.position;
        _startPosition = transform.position;
    }
    
    private void LateUpdate()
    {
        var targetPosition = _player.transform.position + _offset;
        targetPosition.x = _startPosition.x;
        targetPosition.y = _startPosition.y;
        transform.position = targetPosition;
    }
}
