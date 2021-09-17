using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DraggableMenuItemBehaviour : MonoBehaviour
{
    [HideInInspector]
    public bool _closeToSnapPoint = false;
    
    private bool _dragging = false;
    private float _snapDistance = 15.875f;
    private Vector3 _screenPoint, _offset, _savedInputPos, _currentPos, _velocity, _snapVelocity, smoothTarget;
    private float _smoothTime = 0.003f;
    private float _snappingSmoothTime = 0.25f;
    private Vector3 _savedPosition;
    private float _savedYPos;

    private void Update()
    {
        if (!_dragging)
        {
            smoothTarget = transform.position;
            if (_savedPosition.x - transform.position.x > 0.25f)
            {
                smoothTarget.x = (Mathf.FloorToInt(smoothTarget.x / _snapDistance) * _snapDistance);
            }
            else if (_savedPosition.x - transform.position.x < -0.25f)
            {
                smoothTarget.x = (Mathf.CeilToInt(smoothTarget.x / _snapDistance) * _snapDistance);
            }
            smoothTarget.x = Mathf.Clamp(smoothTarget.x, _snapDistance * -3.0f, 0.0f);
            if (Mathf.Abs(smoothTarget.x - transform.position.x) > 0.01f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, smoothTarget, ref _snapVelocity, _snappingSmoothTime);
            }
        }
        if (Mathf.Abs(smoothTarget.x - transform.position.x) > 0.25f)
        {
            _closeToSnapPoint = false;
        }
        else
        {
            _closeToSnapPoint = true;
        }
    }

    void OnMouseDown()
    {
        _savedPosition = transform.position;
        _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        SaveInputPosition();
        _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(_savedInputPos);
    }

    void OnMouseDrag()
    {
        SaveInputPosition();
        _currentPos = Camera.main.ScreenToWorldPoint(_savedInputPos) + _offset;
        _currentPos.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, _currentPos, ref _velocity, _smoothTime);
        _dragging = true;
    }

    private void OnMouseUp()
    {
        _dragging = false;
    }

    private void OnMouseExit()
    {
        _dragging = false;
    }

    void SaveInputPosition()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            _savedInputPos.Set(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _savedInputPos.Set(Input.touches[0].position.x, Input.touches[0].position.y, _screenPoint.z);
        }
    }
 
}