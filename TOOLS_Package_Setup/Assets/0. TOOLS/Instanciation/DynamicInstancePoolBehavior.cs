using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DynamicInstancePoolBehavior : MonoBehaviour
{
    public enum Modes { UseObjectPoolList, UseChildrenAsObjectPool }
    public enum PositionTypes { CreateAtMyPosition, UseOptionalCreationPoint }
    public enum RotationTypes { ZeroOutAllRotations, UseMyRotation, UseOptionalCreationPointRotation}
    
    public Modes mode = Modes.UseObjectPoolList;
    public List<GameObject> objectPoolList = new List<GameObject>();
    public GameObject[] backupPrefabs;
    public PositionTypes positionTypes = PositionTypes.CreateAtMyPosition;
    public RotationTypes rotationType = RotationTypes.ZeroOutAllRotations;
    public bool randomizeOrder = false;
    public bool adoptNewInstanceObjects = true;
    public bool runOnEnable = false;
    public Transform optionalCreationPoint;

    private int _randNum;
    private Vector3 _actualPosition;
    private Quaternion _actualRotation;

    private void Start()
    {
        if (mode == Modes.UseChildrenAsObjectPool)
        {
            objectPoolList.Clear();
            foreach (Transform child in transform)
            {
                objectPoolList.Add(child.gameObject);
            }
        }
    }

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
        for (int i = 0; i < objectPoolList.Count; i++)
        {
            if (!objectPoolList[i].activeSelf)
            {
                ApplyActivation(objectPoolList[i]);
                return;
            }
        }
        InstantiateBackup(backupPrefabs[Random.Range(0, backupPrefabs.Length)]);
    }
    
    public void ActivateRandomPoolInstance()
    {
        
        for (int i = 0; i < 20; i++)
        {
            _randNum = Random.Range(0, objectPoolList.Count);
            if (!objectPoolList[_randNum].activeSelf)
            {
                ApplyActivation(objectPoolList[_randNum]);
                return;
            }
        }
        InstantiateBackup(backupPrefabs[Random.Range(0, backupPrefabs.Length)]);
    }

    private void ApplyActivation(GameObject inputObject)
    {
        SetUpPositionRotation();
        inputObject.transform.position = _actualPosition;
        inputObject.transform.rotation = _actualRotation;
        inputObject.SetActive(true);
    }

    private void InstantiateBackup(GameObject inputObject)
    {
        SetUpPositionRotation();
        GameObject backupObject;
        backupObject = (GameObject)Instantiate(inputObject, _actualPosition, _actualRotation);
        objectPoolList.Add(backupObject);
        if (adoptNewInstanceObjects)
        {
            backupObject.transform.parent = transform;
        }
    }

    private void SetUpPositionRotation()
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
    }
}