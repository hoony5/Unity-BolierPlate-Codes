using UnityEngine;
using UnityEngine.Events;

public class ChangedValueStatus : MonoBehaviour
{
    [SerializeField] private ChangedValueStatusType changedValueStatusType;
    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private float adjustmentMaxValue;
    [SerializeField] private float previousValue;
    [SerializeField] private float threshold = 0.001f;
    [SerializeField] private float currentValue;
    [SerializeField] private UnityEvent<ChangedValueStatus> onChangedValue;

    private void OnEnable()
    {
        previousValue = currentValue = adjustmentMaxValue = maxValue;
    }

    public void AddListener(UnityAction<ChangedValueStatus> listener)
    {
        onChangedValue.AddListener(listener);
    }

    public void RemoveListener(UnityAction<ChangedValueStatus> listener)
    {
        onChangedValue.RemoveListener(listener);
    }

    public void AddValue(float value)
    {
        UpdateCurrentValue(currentValue + value);
    }

    public void MultiplyValue(float value)
    {
        UpdateCurrentValue(currentValue * value);
    }

    public void SetValue(float value)
    {
        if (Mathf.Abs(previousValue - value) <= threshold) 
        {
            UpdateCurrentValue(value);
        }
    }

    private void UpdateCurrentValue(float newValue)
    {
        previousValue = currentValue;
        currentValue = Mathf.Clamp(newValue, minValue, maxValue);
        onChangedValue?.Invoke(this);
    }

    public void AddMaxValue(float value)
    {
        UpdateMaxValue(adjustmentMaxValue + value);
    }

    public void MultiplyMaxValue(float value)
    {
        UpdateMaxValue(adjustmentMaxValue * value);
    }

    public void SetMaxValue(float value)
    {
        UpdateMaxValue(value);
    }

    private void UpdateMaxValue(float newValue)
    {
        adjustmentMaxValue = newValue;
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
