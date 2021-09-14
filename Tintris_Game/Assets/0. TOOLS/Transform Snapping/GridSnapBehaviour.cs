using UnityEngine;

public class GridSnapBehaviour : MonoBehaviour
{
    public enum SnappingTypes { ApplySnapToSelf, ApplySnapToParent }
    
    public bool snapOnEnabled = false;
    public SnappingTypes snappingMode = SnappingTypes.ApplySnapToSelf;
    public float xSnapDistance, ySnapDistance, xOffsetFromZero, yOffsetFromZero;

    private Transform _objectToSnap;
    private Vector3 _savedPos;
    
    private void OnEnable()
    {
        switch (snappingMode)
        {
            case SnappingTypes.ApplySnapToSelf:
                _objectToSnap = transform;
                break;
            case SnappingTypes.ApplySnapToParent:
                _objectToSnap = transform.parent;
                break;
        }
        
        if (snapOnEnabled)
        {
            SnapToGrid();
        }
    }

    public void SnapToGrid()
    {
        _savedPos = _objectToSnap.position;
        _savedPos.x = Mathf.Round(_savedPos.x / xSnapDistance) * xSnapDistance + xOffsetFromZero;
        _savedPos.y = Mathf.Round(_savedPos.y / xSnapDistance) * ySnapDistance + yOffsetFromZero;
        _objectToSnap.position = _savedPos;
    }

    public void MoveUpOneUnit()
    {
        _savedPos = _objectToSnap.position;
        _savedPos.y += ySnapDistance;
        _objectToSnap.position = _savedPos;
    }
    
    public void MoveDownOneUnit()
    {
        _savedPos = _objectToSnap.position;
        _savedPos.y -= ySnapDistance;
        _objectToSnap.position = _savedPos;
    }
    
    public void MoveRightOneUnit()
    {
        _savedPos = _objectToSnap.position;
        _savedPos.x += xSnapDistance;
        _objectToSnap.position = _savedPos;
    }
    
    public void MoveLeftOneUnit()
    {
        _savedPos = _objectToSnap.position;
        _savedPos.x -= xSnapDistance;
        _objectToSnap.position = _savedPos;
    }

    public void MoveAndSnapAlongX(float xMovementDistance)
    {
        _savedPos = _objectToSnap.position;
        _savedPos.x += xMovementDistance;
        _objectToSnap.position = _savedPos;
        SnapToGrid();
    }
    
    public void MoveAndSnapAlongY(float yMovementDistance)
    {
        _savedPos = _objectToSnap.position;
        _savedPos.y += yMovementDistance;
        _objectToSnap.position = _savedPos;
        SnapToGrid();
    }
}
