using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    public void SetActive(bool isActivated)
    {
        gameObject.SetActive(isActivated);
    }
}
