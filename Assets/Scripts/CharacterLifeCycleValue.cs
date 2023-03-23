using UnityEngine;
using UnityEngine.Events;

public class CharacterLifeCycleValue : MonoBehaviour
{
    [SerializeField] private ChangedValueType changedValueType;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private float adjustmentMaxValue;
    [SerializeField] private float previousValue;
    [SerializeField] private float currentValue;
    [SerializeField] private UnityEvent<CharacterLifeCycleValue> onChangedValue;

    private void OnEnable()
    {
        previousValue = currentValue = adjustmentMaxValue = maxValue;
    }

    public void AddListener(UnityAction<CharacterLifeCycleValue> listener)
    {
        onChangedValue.RemoveListener(listener);
        onChangedValue.AddListener(listener);
    }
    public void RemoveListener(UnityAction<CharacterLifeCycleValue> listener)
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
        previousValue = currentValue;
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