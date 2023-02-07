using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private int _timeBetweenSectionGenerationInSeconds;
    [NonSerialized] public double TimerTime;
    
    public void InitTimer()
    {
        TimerTime = Time.time;
    }

    public void StartTimer()
    {
        TimerTime += _timeBetweenSectionGenerationInSeconds;
    }
}
