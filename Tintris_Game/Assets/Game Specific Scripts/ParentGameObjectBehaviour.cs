using UnityEngine;

public class ParentGameObjectBehaviour : MonoBehaviour
{
    public void AttachNewChild(GameObject inputGameObject)
    {
        inputGameObject.transform.parent = transform;
    }
}
