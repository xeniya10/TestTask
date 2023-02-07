using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
            var item = new TMP_Dropdown.OptionData();
            item.text = type;
            _controlDropdown.options.Add(item);
        }
        
        _controlDropdown.captionText.text = _controlDropdown.options[0].text;
        _controlDropdown.onValueChanged.AddListener(_ => DropdownItemSelected());
        
        NotifyObserver(ControlType.Swipe);
    }

    private void DropdownItemSelected()
    {
        _typeWasActivated = _currentType;
        _currentType = (ControlType)_controlDropdown.value;
        NotifyObserver(_currentType);
    }

    public void AddObserver(IObserver observer)
    {
        _observer = observer;
    }

    public void RemoveObserver()
    {
        _observer = null;
    }

    public void NotifyObserver(ControlType type)
    {
        _observer.Update(type);
    }
}
