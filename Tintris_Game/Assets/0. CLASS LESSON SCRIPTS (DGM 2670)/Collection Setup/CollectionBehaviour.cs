using UnityEngine;

public class CollectionBehaviour : MonoBehaviour
{
    public CollectableSO collectedObj;
    public CollectionSO collection;
    
    void Start()
    {
        if (collectedObj.collected)
        {
            EnableDisableBehaviour(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collection.Collect(collectedObj);
        EnableDisableBehaviour(false);
    }

    private void EnableDisableBehaviour(bool value)
    {
        GetComponent<MeshRenderer>().enabled = value;
        GetComponent<Collider>().enabled = value;
    }
}
