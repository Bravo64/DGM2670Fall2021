using UnityEngine;
using UnityEngine.Events;

public class CheckStoredFloatBehaviourV1 : MonoBehaviour
{
    public enum Modes { CompareStoredToFloatData, CompareStoredToSecondVariable}
    public enum AssertionTypes { IsEqualTo, IsGreaterThan, IsLessThan, IsGreaterThanOrEqualTo, IsLessThanOrEqualTo}

    public bool checkOnStart = true;
    public Modes mode = Modes.CompareStoredToFloatData;
    public float storedFloatVariable;
    public AssertionTypes checkFor = AssertionTypes.IsEqualTo;
    public FloatData floatDataObject;
    public float secondFloatVariable;
    public UnityEvent ifStatementIsTrueEvent;
    public UnityEvent elseEvent;

    private float _comparisonValue;

    void Start()
    {
        if (checkOnStart)
        {
            CheckIfTrue();
        }   
    }
    
    public void CheckIfTrue()
    {
        if (mode == Modes.CompareStoredToFloatData)
        {
            _comparisonValue = floatDataObject.value;
        }
        else
        {
            _comparisonValue = secondFloatVariable;
        }
        
        switch (checkFor)
        {
            case AssertionTypes.IsEqualTo:
                if (storedFloatVariable != _comparisonValue)
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsGreaterThan:
                if (storedFloatVariable <= _comparisonValue)
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsLessThan:
                if (storedFloatVariable >= _comparisonValue) 
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsGreaterThanOrEqualTo:
                if (storedFloatVariable < _comparisonValue) 
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsLessThanOrEqualTo:
                if (storedFloatVariable > _comparisonValue)
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
        }
        
        ifStatementIsTrueEvent.Invoke();
        
    }
    
    
    
        public void SetStoredFloat(float number)
        {
            storedFloatVariable = number;
        }
    
        public void SetToZero()
        {
            storedFloatVariable = 0.0f;
        }

        public void Increment()
        {
            storedFloatVariable++;
        }
    
        public void Decrement()
        {
            storedFloatVariable--;
        }
    
        public void Add(float number)
        {
            storedFloatVariable += number;
        }
    
        public void Subtract(float number)
        {
            storedFloatVariable -= number;
        }
    
        public void Randomize(float boundary)
        {
            storedFloatVariable = Random.Range(storedFloatVariable - boundary, storedFloatVariable + boundary);
        }
    
        public void AddRandom(float maxAddition)
        {
            storedFloatVariable = Random.Range(storedFloatVariable, storedFloatVariable + maxAddition);
        }
    
        public void SubtractRandom(float maxSubtraction)
        {
            storedFloatVariable = Random.Range(storedFloatVariable - maxSubtraction, storedFloatVariable);
        }
    
        public void RandomMinZero(float maxstoredFloatVariable)
        {
            storedFloatVariable = Random.Range(0.0f, maxstoredFloatVariable);
        }
    
        public void RandomMinOne(float maxstoredFloatVariable)
        {
            storedFloatVariable = Random.Range(1.0f, maxstoredFloatVariable);
        }
}
