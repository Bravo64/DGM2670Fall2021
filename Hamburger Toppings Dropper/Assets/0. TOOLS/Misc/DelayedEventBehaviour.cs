using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEventBehaviour : MonoBehaviour
{
    public float delayTime = 1.0f;
    public bool beginOnEnable = true;
    public UnityEvent delayedEvent;

    private WaitForSeconds _waitForSecondsObj;

    private void Awake()
    {
        _waitForSecondsObj = new WaitForSeconds(delayTime);
    }

    void OnEnable()
    {
        if (beginOnEnable)
        {
            InitiateCountdown();
        }
    }
    
    public void InitiateCountdown()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return _waitForSecondsObj;
        delayedEvent.Invoke();
    }
}
