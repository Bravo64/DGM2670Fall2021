using System.Collections;
using UnityEngine;

public class SetVelocityZBehaviour : MonoBehaviour
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
            _actualDirection = Vector3.forward;
        }
        else if (directionType == DirectionTypes.Local)
        {
            _actualDirection = transform.forward;
        }

        if (mode == Modes.OnEnable)
        {
            SetZVelocity();
        }
        else if (mode == Modes.ConstantMovement)
        {
            StartCoroutine(SetConstantZMovement());
        }
    }

    public void SetSpeedVariableValue(float inputSpeed)
    {
        speedVariableValue = inputSpeed;
    }

    public void SetZVelocity()
    {
        if (directionType == DirectionTypes.Local)
        {
            _actualDirection = transform.forward;
        }
        if (speedType == SpeedTypes.UseFloatDataSpeed)
        {
            speedVariableValue = floatDataSpeed.value;
        }
        _activeRigidbody.velocity = _actualDirection * speedVariableValue;
    }
    
    public void StartConstantZVelocity()
    {
        StartCoroutine(SetConstantZMovement());
    }

    IEnumerator SetConstantZMovement()
    {
        while (true)
        {
            if (directionType == DirectionTypes.Local)
            {
                _actualDirection = transform.forward;
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
