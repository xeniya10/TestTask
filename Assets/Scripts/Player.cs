using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxDistance = 6;
    [SerializeField] private float _lerpDuration = 1;

    // public void Move()
    // {
    //     Vector3 endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //     StartCoroutine(Lerp(endPosition));
    // }

    private IEnumerator Lerp()
    {
        float currentTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(6f, startPosition.y, startPosition.z);
        
        if (endPosition.x>_maxDistance||endPosition.x<-_maxDistance)
        {
            if (endPosition.x > 0)
            {
                endPosition.x = _maxDistance;
            }
        
            else
            {
                endPosition.x = -_maxDistance;
            }
        }

        while (currentTime < _lerpDuration)
        {
            currentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, Mathf.SmoothStep(0,1, currentTime / _lerpDuration));
            yield return null;
        }
    }
}
