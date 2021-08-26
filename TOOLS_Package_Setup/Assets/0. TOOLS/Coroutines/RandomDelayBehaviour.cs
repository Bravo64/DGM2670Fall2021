using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RandomDelayBehaviour : MonoBehaviour
{
    public float minDelayTime, maxDelayTime;
    public bool runOnEnable = true;
    public UnityEvent delayedEvent;

    private WaitForSeconds _waitForSecondsObj;

    private void Start()
    {
        _waitForSecondsObj = new WaitForSeconds(Random.Range(minDelayTime, maxDelayTime));
    }

    void OnEnable()
    {
        if (runOnEnable)
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
