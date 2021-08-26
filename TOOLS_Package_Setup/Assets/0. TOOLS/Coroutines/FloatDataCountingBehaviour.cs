using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FloatDataCountingBehaviour : MonoBehaviour
{
    public enum Modes { IncreaseOverTime, DecreaseOverTime }

    public FloatData floatDataValueObj;
    public Modes mode = Modes.IncreaseOverTime;
    public float countingSpeedMultiplyer = 1.0f;
    public bool cannotGoNegative = true;
    public bool runOnEnable = true;
    public UnityEvent countdownReachedZeroEvent;

    void OnEnable()
    {
        if (runOnEnable)
        {
            StartTheCount();
        }
    }

    public void StartTheCount()
    {
        if (mode == Modes.IncreaseOverTime)
        {
            StartCoroutine(CountUpTimer());
        }
        else
        {
            if (cannotGoNegative)
            {
                StartCoroutine(CountDownToZero());
            }
            else
            {
                StartCoroutine(CountDownPastNegative());
            }
        }
    }

    public void PauseTheCount()
    {
        StopAllCoroutines();
    }

    IEnumerator CountUpTimer()
    {
        while(true)
        {
            floatDataValueObj.value += countingSpeedMultiplyer * Time.deltaTime;
            yield return 0;
        }
    }

    IEnumerator CountDownToZero()
    {
        while(floatDataValueObj.value > 0.0f)
        {
            floatDataValueObj.value -= countingSpeedMultiplyer * Time.deltaTime;
            yield return 0;
        }
        floatDataValueObj.value = 0.0f;
        countdownReachedZeroEvent.Invoke();
    }
    
    IEnumerator CountDownPastNegative()
    {
        while(true)
        {
            floatDataValueObj.value -= countingSpeedMultiplyer * Time.deltaTime;
            yield return 0;
        }
    }
}
