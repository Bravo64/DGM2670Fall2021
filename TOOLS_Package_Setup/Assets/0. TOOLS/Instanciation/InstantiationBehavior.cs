using UnityEngine;

public class InstantiationBehavior : MonoBehaviour
{
    public enum PositionTypes { CreateAtMyPosition, UseOptionalCreationPoint }
    public enum RotationTypes { ZeroOutAllRotations, UseMyRotation, UseOptionalCreationPointRotation}
    
    public GameObject objectToCreate;
    public PositionTypes positionTypes = PositionTypes.CreateAtMyPosition;
    public RotationTypes rotationType = RotationTypes.ZeroOutAllRotations;
    public bool instantiateOnEnable = false;
    public Transform optionalCreationPoint;

    private Vector3 _actualPosition;
    private Quaternion _actualRotation;
    
    private void OnEnable()
    {
        if (instantiateOnEnable)
        {
            ActivateInstantiation();
        }
    }

    public void SetObjectToCreate(GameObject inputObject)
    {
        objectToCreate = inputObject;
    }

    public void ActivateInstantiation()
    {
        switch (positionTypes)
        {
            case PositionTypes.CreateAtMyPosition:
                _actualPosition = transform.position;
                break;
            case PositionTypes.UseOptionalCreationPoint:
                _actualPosition = optionalCreationPoint.position;
                break;
        }
        switch (rotationType)
        {
            case RotationTypes.ZeroOutAllRotations:
                _actualRotation = Quaternion.identity;
                break;
            case RotationTypes.UseMyRotation:
                _actualRotation = transform.rotation;
                break;
            case RotationTypes.UseOptionalCreationPointRotation:
                _actualRotation = optionalCreationPoint.rotation;
                break;
        }
        Instantiate(objectToCreate, _actualPosition, _actualRotation);
    }
}