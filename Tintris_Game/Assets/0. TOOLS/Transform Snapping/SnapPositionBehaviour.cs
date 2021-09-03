using System;
using UnityEngine;

public class SnapPositionBehaviour : MonoBehaviour
{
    public enum SnappingType { ApplySnapToSelf, ApplySnapToParent }
    public enum Modes { SnapToVector3Reference, SnapToTransformReference, SnapToVector3DataReference }
    public enum SnapAxes { SnapAllAxes, SnapXOnly, SnapYOnly, SnapZOnly }

    public SnappingType snappingType = SnappingType.ApplySnapToSelf;
    public Modes mode = Modes.SnapToVector3Reference;
    public bool vectorIsRelative = false;
    public Vector3 vector3Reference;
    public Transform transformReference;
    public Vector3Data vector3DataReference;
    public SnapAxes snapAxesType = SnapAxes.SnapAllAxes;
    public bool runOnEnable = true;

    private Transform _objectToSnap;
    private Vector3 _savedPos;
    
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
                    ApplyFullPositionSnapping();
                    break;
                case SnapAxes.SnapXOnly:
                    ApplyXOnlyPositionSnapping();
                    break;
                case SnapAxes.SnapYOnly:
                    ApplyYOnlyPositionSnapping();
                    break;
                case SnapAxes.SnapZOnly:
                    ApplyZOnlyPositionSnapping();
                    break;
            }
        }
    }

    public void ApplyFullPositionSnapping()
    {
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative)
                {
                    _objectToSnap.position += vector3Reference;
                }
                else
                {
                    _objectToSnap.position = vector3Reference;
                }
                break;
            case Modes.SnapToTransformReference:
                _objectToSnap.position = transformReference.position;
                break;
            case Modes.SnapToVector3DataReference:
                _objectToSnap.position = vector3DataReference.value;
                break;
        }
    }
    
    public void ApplyXOnlyPositionSnapping()
    {
        _savedPos = _objectToSnap.position;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative) { _savedPos.x =  _savedPos.x + vector3Reference.x; }
                else { _savedPos.x = vector3Reference.x; }
                break;
            case Modes.SnapToTransformReference:
                _savedPos.x = transformReference.position.x;
                break;
            case Modes.SnapToVector3DataReference:
                _savedPos.x = vector3DataReference.value.x;
                break;
        }
        _objectToSnap.position = _savedPos;
    }
    
    public void ApplyYOnlyPositionSnapping()
    {
        _savedPos = _objectToSnap.position;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative) { _savedPos.y =  _savedPos.y + vector3Reference.y; }
                else { _savedPos.y = vector3Reference.y; }
                break;
            case Modes.SnapToTransformReference:
                _savedPos.y = transformReference.position.y;
                break;
            case Modes.SnapToVector3DataReference:
                _savedPos.y = vector3DataReference.value.y;
                break;
        }
        _objectToSnap.position = _savedPos;
    }
    
    public void ApplyZOnlyPositionSnapping()
    {
        _savedPos = _objectToSnap.position;
        switch (mode)
        {
            case Modes.SnapToVector3Reference:
                if (vectorIsRelative) { _savedPos.z =  _savedPos.z + vector3Reference.z; }
                else { _savedPos.z = vector3Reference.z; }
                break;
            case Modes.SnapToTransformReference:
                _savedPos.z = transformReference.position.z;
                break;
            case Modes.SnapToVector3DataReference:
                _savedPos.z = vector3DataReference.value.z;
                break;
        }
        _objectToSnap.position = _savedPos;
    }
}