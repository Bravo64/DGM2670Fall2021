using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateUITextBehaviour : MonoBehaviour
{
    public enum Modes { UseIntData, UseFloatData }

    public bool runOnEnable = true;
    public string valueStringPrefix = "Score: ";
    public string valueStringSuffix;
    public Modes usageMode = Modes.UseIntData;
    public IntData intDataDisplayObj;
    public FloatData floatDataDisplayObj;
    public bool differentForSingular = false;
    public string singleValuePrefix;
    public string singleValueSuffix;

    private TextMeshProUGUI _myTextComponent;
    private string _savedString;
    
    public void OnEnable()
    {
        _myTextComponent = GetComponent<TextMeshProUGUI>();
        if (runOnEnable)
        {
            RefreshUIText();
        }
    }
    
    public void RefreshUIText()
    {
        switch (usageMode)
        {
            case Modes.UseIntData:
                _savedString = intDataDisplayObj.value.ToString();
                break;
            case Modes.UseFloatData:
                _savedString = floatDataDisplayObj.value.ToString(CultureInfo.InvariantCulture);
                break;
        }
        if (!differentForSingular)
        {
            _savedString = valueStringPrefix + _savedString + valueStringSuffix;
        }
        else if (int.Parse(_savedString) == 1)
        {
            _savedString = singleValuePrefix + _savedString + singleValueSuffix;
        }
        else
        {
            _savedString = valueStringPrefix + _savedString + valueStringSuffix;
        }
        
        _myTextComponent.text = _savedString;
    }
}
