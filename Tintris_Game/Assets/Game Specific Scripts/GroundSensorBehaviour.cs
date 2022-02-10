

using UnityEngine;

public class GroundSensorBehaviour : MonoBehaviour
{
    public Rigidbody2D dotRigidbody;
    public IntData somethingIsFalling;

    private Transform _dotParent;
    private bool _floorDetected = false;
    private Vector2 _snappedPos;

    private void Awake()
    {
        if (transform.parent.rotation != Quaternion.identity)
        {
            transform.parent.rotation = Quaternion.identity;
        }
        _dotParent = transform.parent.parent;
        SnapPosition();
    }

    private void Update()
    {
        if (_floorDetected)
        {
            if (dotRigidbody.bodyType != RigidbodyType2D.Kinematic)
            {
                SnapPosition();
                dotRigidbody.bodyType = RigidbodyType2D.Kinematic;
                dotRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                somethingIsFalling.value = 0;
            }
        }
        else
        {
            somethingIsFalling.value = 1;
            if (dotRigidbody.bodyType != RigidbodyType2D.Dynamic)
            {
                SnapPosition();
                dotRigidbody.bodyType = RigidbodyType2D.Dynamic;
                dotRigidbody.constraints = RigidbodyConstraints2D.None;
                dotRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            }
        }
        if (transform.parent.rotation != Quaternion.identity)
        {
            transform.parent.rotation = Quaternion.identity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        somethingIsFalling.value = 0;
        dotRigidbody.bodyType = RigidbodyType2D.Kinematic;
        dotRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        SnapPosition();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        _floorDetected = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        SnapPosition();
        dotRigidbody.bodyType = RigidbodyType2D.Dynamic;
        dotRigidbody.constraints = RigidbodyConstraints2D.None;
        dotRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        dotRigidbody.velocity = Vector2.zero;
        dotRigidbody.AddForce(Vector2.down * 100);
        _floorDetected = false;
    }

    private void LateUpdate()
    {
        _floorDetected = false;
    }

    private void SnapPosition()
    {
        _snappedPos.Set(_dotParent.position.x, Mathf.FloorToInt(_dotParent.position.y) + 0.5f);
        _dotParent.position = _snappedPos;
    }
}
