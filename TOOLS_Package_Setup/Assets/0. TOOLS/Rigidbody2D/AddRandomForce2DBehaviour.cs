using System.Collections;
using UnityEngine;

public class AddRandomForce2DBehaviour : MonoBehaviour
{
    public enum Directions { X, Y }
    public enum DirectionTypes { Global, Local}
    public enum Modes { OnEnable, OnCallOnly, ConstantForce }
    
    public Rigidbody2D activeRigidbody2D;
    public float minAmount, maxAmount;
    public Directions alongAxis = Directions.X;
    public DirectionTypes directionType = DirectionTypes.Global;
    public Modes mode = Modes.ConstantForce;
    
    private Vector3 _direction;
    
    void Start()
    {
        switch (alongAxis)
        {
            case Directions.X:
                _direction = Vector3.right;
                break;
            case Directions.Y:
                _direction = Vector3.up;
                break;
            default:
                _direction = Vector3.right;
                break;
        }
        if (mode == Modes.ConstantForce)
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

    private void OnEnable()
    {
        if (mode == Modes.OnEnable)
        {
            ApplyForce();
        }
    }

    public void ApplyForce()
    {
        float amount = Random.Range(minAmount, maxAmount);
        if (directionType == DirectionTypes.Global)
        {
            activeRigidbody2D.AddForce(amount * _direction);
        }
        else
        {
            activeRigidbody2D.AddRelativeForce(amount * _direction);
        }
    }

    IEnumerator ApplyConstantLocalForce()
    {
        float amount = Random.Range(minAmount, maxAmount);
        while (true)
        {
            activeRigidbody2D.AddForce(amount * _direction);
            yield return 0;
        }
    }
    
    IEnumerator ApplyConstantGlobalForce()
    {
        float amount = Random.Range(minAmount, maxAmount);
        while (true)
        {
            activeRigidbody2D.AddForce(amount * _direction);
            yield return 0;
        }
    }
}
