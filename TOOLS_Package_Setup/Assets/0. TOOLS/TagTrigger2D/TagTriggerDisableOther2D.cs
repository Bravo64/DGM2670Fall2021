using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class TagTriggerDisableOther2D : MonoBehaviour
{
    public string tagName;
    public bool disableOtherParent = false;
    private void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tagName)) { return; }
        
        if (disableOtherParent)
        {
            other.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            other.gameObject.SetActive(false);
        }
    }
}
