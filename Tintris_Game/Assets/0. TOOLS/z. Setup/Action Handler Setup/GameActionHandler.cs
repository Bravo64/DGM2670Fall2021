using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{
    public GameAction action;
    public UnityEvent startEvent, respondEvent, respondLateEvent;
    public float holdTime = 0.1f;

    private WaitForSeconds waitObj;
    
    void Start()
    {
        startEvent.Invoke();
    }

    private void OnEnable()
    {
        waitObj = new WaitForSeconds(holdTime);
        action.raiseNoArgs += Respond;
    }

    private void Respond()
    {
        respondEvent.Invoke();
        StartCoroutine(LateRespond());
    }
    
    private IEnumerator LateRespond()
    {
        yield return waitObj;
        respondLateEvent.Invoke();
    }
}