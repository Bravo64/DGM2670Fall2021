using System.Collections;
using UnityEngine;

public class SetVelocityX2DBehaviour : MonoBehaviour
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
    
    private Rigidbody2D _activeRigidbody2D;
    private Vector2 _actualDirection;

    void OnEnable()
    {
        switch (rigidbodyMode)
        {
            case RigidbodyMode.UseMyRigidbody:
                _activeRigidbody2D = GetComponent<Rigidbody2D>();
                break;
            case RigidbodyMode.UseParentRigidbody:
                _activeRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
                break;
        }

        if (directionType == DirectionTypes.Global)
        {
            _actualDirection = Vector2.right;
        }
        else if (directionType == DirectionTypes.Local)
        {
            _actualDirection = transform.right;
        }

        if (mode == Modes.OnEnable)
        {
            SetXVelocity();
        }
        else if (mode == Modes.ConstantMovement)
        {
            StartCoroutine(SetConstantXMovement());
        }
    }

    public void SetSpeedVariableValue(float inputSpeed)
    {
        speedVariableValue = inputSpeed;
    }

    public void SetXVelocity()
    {
        if (directionType == DirectionTypes.Local)
        {
            _actualDirection = transform.right;
        }
        if (speedType == SpeedTypes.UseFloatDataSpeed)
        {
            speedVariableValue = floatDataSpeed.value;
        }
        _activeRigidbody2D.velocity = _actualDirection * speedVariableValue;
    }
    
    public void StartConstantXVelocity()
    {
        StartCoroutine(SetConstantXMovement());
    }

    IEnumerator SetConstantXMovement()
    {
        while (true)
        {
            if (directionType == DirectionTypes.Local)
            {
                _actualDirection = transform.right;
            }
            _activeRigidbody2D.velocity = _actualDirection * speedVariableValue;
            if (speedType == SpeedTypes.UseFloatDataSpeed)
            {
                speedVariableValue = floatDataSpeed.value;
            }
            _activeRigidbody2D.velocity = _actualDirection * speedVariableValue;
            yield return 0;
        }
    }
}
