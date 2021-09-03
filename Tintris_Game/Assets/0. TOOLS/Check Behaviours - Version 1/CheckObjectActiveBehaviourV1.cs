using UnityEngine;
using UnityEngine.Events;

public class CheckObjectActiveBehaviourV1 : MonoBehaviour
{
    public enum AssertionTypes { IsActive, IsNotActive }
    
    public bool checkOnStart = true;
    public bool alsoCheckUpperHierarchy = false;
    public GameObject objectToCheck;
    public AssertionTypes checkFor = AssertionTypes.IsActive;
    public UnityEvent stateIsTrueEvent;
    public UnityEvent elseEvent;

    private void Start()
    {
        if (checkOnStart)
        {
            CheckObjectState();
        }
    }

    public void CheckObjectState()
    {
        switch (checkFor)
        {
            case AssertionTypes.IsActive:
                if (!objectToCheck.activeSelf || alsoCheckUpperHierarchy && !objectToCheck.activeInHierarchy)
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
            case AssertionTypes.IsNotActive:
                if (!objectToCheck.activeSelf || alsoCheckUpperHierarchy && !objectToCheck.activeInHierarchy) 
                {
                    elseEvent.Invoke();
                    return;
                }
                break;
        }
        stateIsTrueEvent.Invoke();
    }
}
