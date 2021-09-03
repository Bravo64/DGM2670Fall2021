using UnityEngine;
using UnityEngine.Events;

public class ToggleEventPairBehaviour : MonoBehaviour
{
    public enum EventStatus { MainReady, SecondaryReady }

    public EventStatus currentlyActiveEvent = EventStatus.MainReady;
    public UnityEvent eventMain;
    public UnityEvent eventSecondary;
    public bool runOnEnable = true;
    
    void OnEnable()
    {
        if (runOnEnable)
        {
            InvokeOneOfToggleEventPair();
        }
    }

    public void InvokeOneOfToggleEventPair()
    {
        if (currentlyActiveEvent == EventStatus.MainReady)
        {
            eventMain.Invoke();
            currentlyActiveEvent = EventStatus.SecondaryReady;
        }
        else
        {
            eventSecondary.Invoke();
            currentlyActiveEvent = EventStatus.MainReady;
        }
    }
}
