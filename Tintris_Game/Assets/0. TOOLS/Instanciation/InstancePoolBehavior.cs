using UnityEngine;

public class InstancePoolBehavior : MonoBehaviour
{
    public enum PositionTypes { CreateAtMyPosition, UseOptionalCreationPoint }
    public enum RotationTypes { ZeroOutAllRotations, UseMyRotation, UseOptionalCreationPointRotation}
    
    public GameObject[] objectPool;
    public PositionTypes positionTypes = PositionTypes.CreateAtMyPosition;
    public RotationTypes rotationType = RotationTypes.ZeroOutAllRotations;
    public bool randomizeOrder = false;
    public bool runOnEnable = false;
    public Transform optionalCreationPoint;

    private int _randNum;
    private Vector3 _actualPosition;
    private Quaternion _actualRotation;

    private void OnEnable()
    {
        if (runOnEnable)
        {
            if (!randomizeOrder)
            {
                ActivatePoolInstance();
            }
            else
            {
                ActivateRandomPoolInstance();
            }
        }
    }

    public void ActivatePoolInstance()
    {
        for (int i = 0; i < objectPool.Length; i++)
        {
            if (!objectPool[i].activeSelf)
            {
                ApplyActivation(objectPool[i]);
                break;
            }
        }
    }
    
    public void ActivateRandomPoolInstance()
    {
        
        for (int i = 0; i < 100; i++)
        {
            _randNum = Random.Range(0, objectPool.Length);
            if (!objectPool[_randNum].activeSelf)
            {
                ApplyActivation(objectPool[_randNum]);
                break;
            }
        }
    }

    private void ApplyActivation(GameObject inputObject)
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
        
        inputObject.transform.position = _actualPosition;
        inputObject.transform.rotation = _actualRotation;
        inputObject.SetActive(true);
    }
}