using UnityEngine;
using UnityEngine.Events;

public class LifeCycleValue : MonoBehaviour
{
    [SerializeField] private LifeValueType lifeValueType;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private float adjustmentMaxValue;
    [SerializeField] private float previousValue;
    [SerializeField] private float threshold = 0.001f;
    [SerializeField] private float currentValue;
    [SerializeField] private UnityEvent<LifeCycleValue> onChangedValue;

    private void OnEnable()
    {
        previousValue = currentValue = adjustmentMaxValue = maxValue;
    }

    public void AddListener(UnityAction<LifeCycleValue> listener)
    {
        onChangedValue.RemoveListener(listener);
        onChangedValue.AddListener(listener);
    }
    public void RemoveListener(UnityAction<LifeCycleValue> listener)
    {
        onChangedValue.RemoveListener(listener);
    }

    public void AddValue(float value)
    {
        previousValue = currentValue;
        onChangedValue?.Invoke(this);
        currentValue += value;

        if (currentValue > maxValue)
            currentValue = maxValue;
        
        if (currentValue < minValue)
            currentValue = minValue;
    }
    public void MultiplyValue(float value)
    {
        previousValue = currentValue;
        onChangedValue?.Invoke(this);
        currentValue *= value;

        if (currentValue > maxValue)
            currentValue = maxValue;
        
        if (currentValue < minValue)
            currentValue = minValue;
    }
    public void SetValue(float value)
    {
        if (Mathf.Abs(previousValue - value) > threshold) return;
        
        onChangedValue?.Invoke(this);
        currentValue = value;

        if (currentValue > maxValue)
            currentValue = maxValue;
        
        if (currentValue < minValue)
            currentValue = minValue;
    }

    public void AddMaxValue(float value)
    {
        adjustmentMaxValue += value;
    }
    public void MultiplyMaxValue(float value)
    {
        adjustmentMaxValue *= value;
    }
    public void SetMaxValue(float value)
    {
        adjustmentMaxValue = value;
    }
    // original Value is only changed by status 
    public void ResetToOriginalValue()
    {
        currentValue = maxValue;
        previousValue = maxValue;
    }
    // Adjustment Value is only used on the Battle
    public void ResetToAdjustmentValue()
    {
        currentValue = adjustmentMaxValue;
        previousValue = adjustmentMaxValue;
    }
}