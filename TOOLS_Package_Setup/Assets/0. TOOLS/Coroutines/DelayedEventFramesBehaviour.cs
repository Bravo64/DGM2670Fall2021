using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEventFramesBehaviour : MonoBehaviour
{
    public int numberOfWaitFrames = 1;
    public bool runOnEnable = true;
    public UnityEvent delayedFramesEvent;

    void OnEnable()
    {
        if (runOnEnable)
        {
            InitiateFrameWait();
        }
    }
    
    public void InitiateFrameWait()
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
        yield return numberOfWaitFrames - 1;
        if (!gameObject.activeSelf)
        {
            StopAllCoroutines();
        }
        delayedFramesEvent.Invoke();
    }
}
