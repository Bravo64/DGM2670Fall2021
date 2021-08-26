using System.Collections;
using UnityEngine;

public class SetVelocityYBehaviour : MonoBehaviour
{
    public enum RigidbodyMode { UseMyRigidbody, UseParentRigidbody }
    public enum DirectionTypes { Global, Local}
    public enum Modes { OnEnable, OnCallOnly, ConstantMovement }
    public enum SpeedTypes { UseSpeedVariableValue, UseFloatDataSpeed }

    public RigidbodyMode rigidbodyMode = RigidbodyMode.UseMyRigidbody;
    public SpeedTypes speedType = SpeedTypes.UseSpeedVariableValue;
    public float speedVariableValue;
    public FloatData floatDataSpeed;
    public DirectionTypes directionType = DirectionTypes.Global;
    public Modes mode = Modes.ConstantMovement;
    
    private Rigidbody _activeRigidbody;
    private Vector3 _actualDirection;

    void OnEnable()
    {
        switch (rigidbodyMode)
        {
            case RigidbodyMode.UseMyRigidbody:
                _activeRigidbody = GetComponent<Rigidbody>();
                break;
            case RigidbodyMode.UseParentRigidbody:
                _activeRigidbody = transform.parent.GetComponent<Rigidbody>();
                break;
        }

        if (directionType == DirectionTypes.Global)
        {
            _actualDirection = Vector3.up;
        }
        else if (directionType == DirectionTypes.Local)
        {
            _actualDirection = transform.up;
        }

        if (mode == Modes.OnEnable)
        {
            SetYVelocity();
        }
        else if (mode == Modes.ConstantMovement)
        {
            StartCoroutine(SetConstantYMovement());
        }
    }

    public void SetSpeedVariableValue(float inputSpeed)
    {
        speedVariableValue = inputSpeed;
    }

    public void SetYVelocity()
    {
        if (directionType == DirectionTypes.Local)
        {
            _actualDirection = transform.up;
        }
        if (speedType == SpeedTypes.UseFloatDataSpeed)
        {
            speedVariableValue = floatDataSpeed.value;
        }
        _activeRigidbody.velocity = _actualDirection * speedVariableValue;
    }
    
    public void StartConstantYVelocity()
    {
        StartCoroutine(SetConstantYMovement());
    }

    IEnumerator SetConstantYMovement()
    {
        while (true)
        {
            if (directionType == DirectionTypes.Local)
            {
                _actualDirection = transform.up;
            }
            _activeRigidbody.velocity = _actualDirection * speedVariableValue;
            if (speedType == SpeedTypes.UseFloatDataSpeed)
            {
                speedVariableValue = floatDataSpeed.value;
            }
            _activeRigidbody.velocity = _actualDirection * speedVariableValue;
            yield return 0;
        }
    }
}
