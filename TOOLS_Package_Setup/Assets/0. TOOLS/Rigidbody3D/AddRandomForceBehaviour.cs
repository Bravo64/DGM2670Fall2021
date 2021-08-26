using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddRandomForceBehaviour : MonoBehaviour
{
    public enum Directions { X, Y, Z }
    public enum DirectionTypes { Global, Local}
    public enum Modes { OnStart, OnCallOnly, ConstantForce }
    
    public float minAmount, maxAmount;
    public Directions alongAxis = Directions.X;
    public DirectionTypes directionType = DirectionTypes.Global;
    public Modes mode = Modes.ConstantForce;

    private Rigidbody _myRigidbody;
    private Vector3 _direction;
    
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();

        switch (alongAxis)
        {
            case Directions.X:
                _direction = Vector3.right;
                break;
            case Directions.Y:
                _direction = Vector3.up;
                break;
            case Directions.Z:
                _direction = Vector3.forward;
                break;
            default:
                _direction = Vector3.right;
                break;
        }

        if (mode == Modes.OnStart)
        {
            ApplyForce();
        }
        else if (mode == Modes.ConstantForce)
        {
            if (directionType == DirectionTypes.Global)
            {
                StartCoroutine(ApplyConstantGlobalForce());
            }
            else
            {
                StartCoroutine(ApplyConstantLocalForce());
            }
        }
    }

    public void ApplyForce()
    {
        float amount = Random.Range(minAmount, maxAmount);
        if (directionType == DirectionTypes.Global)
        {
            _myRigidbody.AddForce(amount * _direction);
        }
        else
        {
            _myRigidbody.AddRelativeForce(amount * _direction);
        }
    }

    IEnumerator ApplyConstantLocalForce()
    {
        float amount = Random.Range(minAmount, maxAmount);
        while (true)
        {
            _myRigidbody.AddForce(amount * _direction);
            yield return 0;
        }
    }
    
    IEnumerator ApplyConstantGlobalForce()
    {
        float amount = Random.Range(minAmount, maxAmount);
        while (true)
        {
            _myRigidbody.AddForce(amount * _direction);
            yield return 0;
        }
    }
}
