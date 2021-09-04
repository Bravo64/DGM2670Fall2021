using UnityEngine;

[CreateAssetMenu(fileName = "Time Scale Setter Obj", menuName = "Misc/Time Scale Setter Obj")]
public class TimeScaleSetter : ScriptableObject
{
    public FloatData timeScaleObj;

    void Awake()
    {
        timeScaleObj.value = Time.timeScale;
    }
    
    public void PauseTime()
    {
        Time.timeScale = 0.0f;
        timeScaleObj.value = 0.0f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1.0f;
        timeScaleObj.value = 1.0f;
    }

    public void SetTimeScale(float inputValue)
    {
        Time.timeScale = inputValue;
        timeScaleObj.value = inputValue;
    }
}
