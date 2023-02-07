using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class RoadManager : BaseObject
{
    [SerializeField] private Transform _roadSection;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private int _numberOfSections;

    private Timer _timer;
    private List<Transform> _sections = new List<Transform>();

    [Inject] public void Inject(Timer timer)
    {
        _timer = timer;
    }
    
    private void OnEnable()
    {
        if (_sections.Count == 0)
        {
            GenerateSections();
        }
    }

    private void Update()
    {
        if (Time.time > _timer.TimerTime)
        {
            MoveSection();
        }
    }

    private void GenerateSections()
    {
        for (var i = 0; i < _numberOfSections; i++)
        {
            var section = Instantiate(_roadSection, transform);
            _roadSection.localScale = _scale;
            SetPosition(section);
            _sections.Add(section);
        }

        _timer.InitTimer();
        _timer.StartTimer();
    }

    private void MoveSection()
    {
        var firstSection = FindFirstSection();
        SetPosition(firstSection);
        _timer.StartTimer();
    }

    private void SetPosition(Transform section)
    {
        _startPosition.z += _scale.z;
        section.localPosition = _startPosition;
    }

    private Transform FindFirstSection()
    {
        // Lazy solution
        // var minZ = _sections.Min(section => section.localPosition.z);
        // var firstSection = _sections.Find(section => section.localPosition.z == minZ);
        
        Transform firstSection = null;
        
        foreach (var section in _sections)
        {
            if (firstSection == null || section.position.z < firstSection.position.z)
            {
                firstSection = section;
            }
        }

        return firstSection;
    }
}
