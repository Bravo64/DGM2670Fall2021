using UnityEngine;

public class SnapScaleBehaviour : MonoBehaviour
{
    public enum SnappingType { ApplySnapToSelf, ApplySnapToParent }
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }
    public enum SnapAxes { SnapAllAxes, SnapXOnly, SnapYOnly, SnapZOnly }
    
    public SnappingType snappingType = SnappingType.ApplySnapToSelf;
    public Modes mode = Modes.SnapToVector3Reference;
    public bool vectorIsRelativelyMultiplied = false;
    public Vector3 vector3Reference;
    public Transform transformReference;
    public Vector3Data vector3DataReference;
    public SnapAxes snapAxesType = SnapAxes.SnapAllAxes;
    public bool runOnEnable = true;

    private Transform _objectToSnap;
    private Vector3 _savedScale;
    
    void OnEnable()
    {
        switch (snappingType)
        {
            case SnappingType.ApplySnapToSelf:
                _objectToSnap = transform;
                break;
            case SnappingType.ApplySnapToParent:
                _objectToSnap = transform.parent;
                break;
        }
        
        if (runOnEnable)
        {
            switch (snapAxesType)
            {
                case SnapAxes.SnapAllAxes:
                    ApplyFullScaleSnapping();
                    break;
                case SnapAxes.SnapXOnly:
                    ApplyXOnlyScaleSnapping();
                    break;
                case SnapAxes.SnapYOnly:
                    ApplyYOnlyScaleSnapping();
                    break;
                case SnapAxes.SnapZOnly:
                    ApplyZOnlyScaleSnapping();
                    break;
            }
        }
    }

    public void ApplyFullScaleSnapping()
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelativelyMultiplied)
                {
                    _savedScale = _objectToSnap.localScale;
                    _objectToSnap.localScale = new Vector3(vector3Reference.x * _savedScale.x, vector3Reference.y * _savedScale.y, vector3Reference.z * _savedScale.z);
                }
                else
                {
                    _objectToSnap.localScale = vector3Reference;
                }
                break;
            case Modes.SnapToTransformReference:
                _objectToSnap.localScale = transformReference.localScale;
                break;
            case Modes.SnapToVector3DataReference:
                _objectToSnap.localScale = vector3DataReference.value;
                break;
            default:
                _savedScale = _objectToSnap.localScale;
                _objectToSnap.localScale = new Vector3(vector3Reference.x * _savedScale.x, vector3Reference.y * _savedScale.y, vector3Reference.z * _savedScale.z);
                break;
        }
    }
    
    public void ApplyXOnlyScaleSnapping()
    {
        _savedScale = _objectToSnap.localScale;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelativelyMultiplied) { _savedScale.x = _savedScale.x * vector3Reference.x; }
                else { _savedScale.x = vector3Reference.x; }
                break;
            case Modes.SnapToTransformReference:
                _savedScale.x = transformReference.position.x;
                break;
            case Modes.SnapToVector3DataReference:
                _savedScale.x = vector3DataReference.value.x;
                break;
        }
        _objectToSnap.localScale = _savedScale;
    }
    
    public void ApplyYOnlyScaleSnapping()
    {
        _savedScale = _objectToSnap.localScale;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelativelyMultiplied) { _savedScale.y = _savedScale.y * vector3Reference.y; }
                else { _savedScale.y = vector3Reference.y; }
                break;
            case Modes.SnapToTransformReference:
                _savedScale.y = transformReference.position.y;
                break;
            case Modes.SnapToVector3DataReference:
                _savedScale.y = vector3DataReference.value.y;
                break;
        }
        _objectToSnap.localScale = _savedScale;
    }
    
    public void ApplyZOnlyScaleSnapping()
    {
        _savedScale = _objectToSnap.localScale;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelativelyMultiplied) { _savedScale.z = _savedScale.z * vector3Reference.z; }
                else { _savedScale.z = vector3Reference.z; }
                break;
            case Modes.SnapToTransformReference:
                _savedScale.z = transformReference.position.z;
                break;
            case Modes.SnapToVector3DataReference:
                _savedScale.z = vector3DataReference.value.z;
                break;
        }
        _objectToSnap.localScale = _savedScale;
    }
}
