using UnityEngine;

public class ComboSensorBehaviour : MonoBehaviour
{
    public DotBehaviour dotParentScript;

    private Transform _dotParent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            _dotParent = other.transform.parent;
            if (!dotParentScript.potentialCombos.Contains(_dotParent.gameObject))
            {
                if (dotParentScript.myRigidbody2D.bodyType == RigidbodyType2D.Kinematic && dotParentScript._gravityActivated)
                {
                    if (_dotParent.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
                    {
                        if (dotParentScript._gravityActivated)
                        {
                            dotParentScript.potentialCombos.Add(_dotParent.gameObject);
                        }
                    }
                    if (dotParentScript.gameObject.layer == LayerMask.NameToLayer("Landed Dot"))
                    {
                        dotParentScript.ShapeLandedEvent();
                    }
                }
            }
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            _dotParent = other.transform.parent;
            if (!dotParentScript.potentialCombos.Contains(_dotParent.gameObject))
            {
                if (dotParentScript.myRigidbody2D.bodyType == RigidbodyType2D.Kinematic && dotParentScript._gravityActivated)
                {
                    if (_dotParent.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Kinematic)
                    {
                        if (dotParentScript._gravityActivated)
                        {
                            dotParentScript.potentialCombos.Add(_dotParent.gameObject);
                        }
                    }
                    if (dotParentScript.gameObject.layer == LayerMask.NameToLayer("Landed Dot"))
                    {
                        dotParentScript.ShapeLandedEvent();
                    }
                }
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            if (dotParentScript.potentialCombos.Contains(other.transform.parent.gameObject))
            {
                dotParentScript.potentialCombos.Remove(other.transform.parent.gameObject);
            }
        }
    }
}
