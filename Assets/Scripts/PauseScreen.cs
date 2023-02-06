using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ControlType { Swipe, Drag, Arrow }

public class PauseScreen : BaseScreen, IObservable
{
    [SerializeField] private TMP_Dropdown _controlDropdown;
    [SerializeField] private ControlType _typeWasActivated;

    private IObserver _observer;
    private ControlType _currentType;
    private List<string> _controlTypes = new List<string>();

    public void CreateDropdown()
    {
        _controlDropdown.options.Clear();

        _controlTypes.Add($"{ControlType.Swipe} Control");
        _controlTypes.Add($"{ControlType.Drag} Control");
        _controlTypes.Add($"{ControlType.Arrow} Control");

        foreach (var type in _controlTypes)
        {
            _controlDropdown.options.Add(new TMP_Dropdown.OptionData() {text = type}) ;
        }
        
        _controlDropdown.captionText.text = _controlDropdown.options[0].text;
        _controlDropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(_controlDropdown);});
        
        NotifyObservers(ControlType.Swipe);
    }

    private void DropdownItemSelected(TMP_Dropdown dropdown)
    {
        _typeWasActivated = _currentType;
        _currentType = (ControlType)_controlDropdown.value;
        NotifyObservers(_currentType);
    }

    public void AddObserver(IObserver observer)
    {
        _observer = observer;
    }

    public void RemoveObserver()
    {
        _observer = null;
    }

    public void NotifyObservers(ControlType type)
    {
        _observer.ChangeControl(type);
    }
}
