using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ShapeSensorBehaviour : MonoBehaviour
{
    public ShapeMovementBehaviour parentShapeScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Mathf.Abs(transform.eulerAngles.z) < 2.0f)
        {
            if (other.CompareTag("Side Boundary"))
            {
                if (transform.position.x > 0)
                {
                    parentShapeScript.transform.position += Vector3.left;
                }
                else
                {
                    parentShapeScript.transform.position += Vector3.right;
                }
            }
            else
            {
                parentShapeScript.groundDetected = true;
            }
        }
        else if (Mathf.Abs(90 - transform.eulerAngles.z) < 2.0f)
        {
            parentShapeScript.rightMovementLocked = true;
        }
        else if (Mathf.Abs(270 - transform.eulerAngles.z) < 2.0f)
        {
            parentShapeScript.leftMovementLocked = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Mathf.Abs(transform.eulerAngles.z) < 2.0f)
        {
            if (other.CompareTag("Side Boundary"))
            {
                if (transform.position.x > 0)
                {
                    parentShapeScript.transform.position += Vector3.left;
                }
                else
                {
                    parentShapeScript.transform.position += Vector3.right;
                }
            }
            else
            {
                parentShapeScript.groundDetected = true;
            }
        }
        else if (Mathf.Abs(90 - transform.eulerAngles.z) < 2.0f)
        {
            parentShapeScript.rightMovementLocked = true;
        }
        else if (Mathf.Abs(270 - transform.eulerAngles.z) < 2.0f)
        {
            parentShapeScript.leftMovementLocked = true;
        }
    }
}
