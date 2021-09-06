using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEventTimeBehaviourV2 : MonoBehaviour
{
    public float delayTime = 1.0f;
    public bool runOnEnable = true;
    public UnityEvent delayedTimeEvent;

    private WaitForSeconds _waitForSecondsObj;

    void Awake()
    {
        _waitForSecondsObj = new WaitForSeconds(delayTime);
    }

    private void OnEnable()
    {
        if (runOnEnable)
        {
            InitiateCountdown();
        }
    }

    public void InitiateCountdown()
    {
        if (!gameObject.activeSelf)
        {
            StopAllCoroutines();
            return; 
        }
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return _waitForSecondsObj;
        if (!gameObject.activeSelf)
        {
            StopAllCoroutines();
        }
        delayedTimeEvent.Invoke();
    }
}
