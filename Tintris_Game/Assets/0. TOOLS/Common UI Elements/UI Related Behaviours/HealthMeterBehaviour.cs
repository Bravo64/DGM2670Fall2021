using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthMeterBehaviour : MonoBehaviour
{
    public Image childMeterImage;
    public FloatData healthValueHolder;
    public UnityEvent meterEmptyEvent;

    private Vector3 _currentScale;
    private Color _currentColor;

    private void Start()
    {
        childMeterImage.color = Color.green;
        UpdateMeterUI();
    }

    public void UpdateMeterUI()
    {
        healthValueHolder.value = Mathf.Clamp(healthValueHolder.value, 0.0f, 1.0f);
        _currentScale = transform.localScale;
        _currentScale.x = healthValueHolder.value;
        transform.localScale = _currentScale;
        
        _currentColor = childMeterImage.color;
        _currentColor.g = healthValueHolder.value;
        _currentColor.r = 1.6f - healthValueHolder.value;
        childMeterImage.color = _currentColor;
        
        if (healthValueHolder.value <= 0)
        {
            childMeterImage.color = Color.red;
            meterEmptyEvent.Invoke();
        }
    }
}
