using UnityEngine;

public class DotErrorSensorBehaviour : MonoBehaviour
{
    public Transform dotParent;
    public Rigidbody2D _dotRigidbody;

    private Collider2D _myBlock;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == _myBlock) { return; }
        
        if (_myBlock == null)
        {
            _myBlock = other;
        }
        else if (other != _myBlock && gameObject.layer == LayerMask.NameToLayer("Landed Dot"))
        {
            dotParent.position = new Vector2(dotParent.position.x, Mathf.FloorToInt(dotParent.position.y + 1.0f));
            _dotRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
