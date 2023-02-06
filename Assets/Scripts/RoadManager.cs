using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : BaseObject
{
    [SerializeField] private Transform _roadSection;
    [SerializeField] private int _sectionGenerationTimer;
    [SerializeField] private int _sectionDestructionTimer;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _scale;
    [SerializeField] private int _numberOfSections;

    private List<Transform> _sections = new List<Transform>();

    private void OnEnable()
    {
        if (_sections.Count == 0)
        {
            GenerateSections();
        }
        
        StartCoroutine(GenerateSection());
    }

    private void GenerateSections()
    {
        for (var i = 0; i < _numberOfSections; i++)
        {
            var section = Instantiate(_roadSection, transform);
            section.gameObject.SetActive(false);
            _sections.Add(section);
        }
    }

    private IEnumerator GenerateSection()
    {
        while (true)
        {
            _roadSection.localScale = _scale;
            _startPosition.z += _scale.z;
            // section.localPosition = _startPosition;

            yield return new WaitForSeconds(_sectionGenerationTimer);
        }
    }
}
