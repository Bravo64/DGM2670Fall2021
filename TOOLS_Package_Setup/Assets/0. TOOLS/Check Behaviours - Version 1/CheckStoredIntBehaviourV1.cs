using UnityEngine;
using UnityEngine.Events;

public class CheckStoredIntBehaviourV1 : MonoBehaviour
{
    public enum Modes { CompareStoredToIntData, CompareStoredToSecondVariable}
    public enum AssertionTypes { IsEqualTo, IsGreaterThan, IsLessThan, IsGreaterThanOrEqualTo, IsLessThanOrEqualTo}

    public bool checkOnStart = true;
    public Modes mode = Modes.CompareStoredToIntData;
    public int storedIntVariable;
    public AssertionTypes checkFor = AssertionTypes.IsEqualTo;
    public IntData intDataObject;
    public int secondIntVariable;
    public UnityEvent ifStatementIsTrueEvent;
    public UnityEvent elseEvent;

    private int _comparisonValue;

    void Start()
    {
        if (checkOnStart)
        {
            CheckIfTrue();
        }   
    }
    
    public void CheckIfTrue()
    {
        if (mode == Modes.CompareStoredToIntData)
        {
            _comparisonValue = intDataObject.value;
        }
        else
        {
            _comparisonValue = secondIntVariable;
        }
        
        switch (checkFor)
        {
            case AssertionTypes.IsEqualTo:
                if (storedIntVariable != _comparisonValue)
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsGreaterThan:
                if (storedIntVariable <= _comparisonValue)
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsLessThan:
                if (storedIntVariable >= _comparisonValue) 
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsGreaterThanOrEqualTo:
                if (storedIntVariable < _comparisonValue) 
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsLessThanOrEqualTo:
                if (storedIntVariable > _comparisonValue)
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
        }
        
        ifStatementIsTrueEvent.Invoke();
        
    }
    
    
    
        public void SetStoredInt(int number)
        {
            storedIntVariable = number;
        }
    
        public void SetToZero()
        {
            storedIntVariable = 0;
        }

        public void Increment()
        {
            storedIntVariable++;
        }
    
        public void Decrement()
        {
            storedIntVariable--;
        }
    
        public void Add(int number)
        {
            storedIntVariable += number;
        }
    
        public void Subtract(int number)
        {
            storedIntVariable -= number;
        }
    
        public void Randomize(int boundary)
        {
            storedIntVariable = Random.Range(storedIntVariable - boundary, storedIntVariable + boundary);
        }
    
        public void AddRandom(int maxAddition)
        {
            storedIntVariable = Random.Range(storedIntVariable, storedIntVariable + maxAddition);
        }
    
        public void SubtractRandom(int maxSubtraction)
        {
            storedIntVariable = Random.Range(storedIntVariable - maxSubtraction, storedIntVariable);
        }
    
        public void RandomMinZero(int maxstoredIntVariable)
        {
            storedIntVariable = Random.Range(0, maxstoredIntVariable);
        }
    
        public void RandomMinOne(int maxstoredIntVariable)
        {
            storedIntVariable = Random.Range(1, maxstoredIntVariable);
        }
}
