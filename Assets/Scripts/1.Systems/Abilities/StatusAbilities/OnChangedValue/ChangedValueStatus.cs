using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChangedValueStatus : MonoBehaviour
{
    [field:SerializeField] private ChangedValueStatusType StatusType { get; set; }
    [field:SerializeField] private float MinValue { get; set; }
    [field:SerializeField] private float MaxValue { get; set; }
    [field:SerializeField] private float AdjustmentMaxValue { get; set; }
    [field:SerializeField] private float PreviousValue { get; set; }
    [field:SerializeField] private float Threshold { get; set; } = 0.001f;
    [field:SerializeField] private float CurrentValue { get; set; }
    [field:SerializeField] private UnityEvent<ChangedValueStatus> OnChangedValue { get; set; }

    private void OnEnable()
    {
        PreviousValue = CurrentValue = AdjustmentMaxValue = MaxValue;
    }

    public void AddListener(UnityAction<ChangedValueStatus> listener)
    {
        OnChangedValue.AddListener(listener);
    }

    public void RemoveListener(UnityAction<ChangedValueStatus> listener)
    {
        OnChangedValue.RemoveListener(listener);
    }

    public void AddValue(float value)
    {
        UpdateCurrentValue(CurrentValue + value);
    }

    public void MultiplyValue(float value)
    {
        UpdateCurrentValue(CurrentValue * value);
    }

    public void SetValue(float value)
    {
        if (Mathf.Abs(PreviousValue - value) <= Threshold) 
        {
            UpdateCurrentValue(value);
        }
    }

    private void UpdateCurrentValue(float newValue)
    {
        PreviousValue = CurrentValue;
        CurrentValue = Mathf.Clamp(newValue, MinValue, MaxValue);
        OnChangedValue?.Invoke(this);
    }

    public void AddMaxValue(float value)
    {
        UpdateMaxValue(AdjustmentMaxValue + value);
    }

    public void MultiplyMaxValue(float value)
    {
        UpdateMaxValue(AdjustmentMaxValue * value);
    }

    public void SetMaxValue(float value)
    {
        UpdateMaxValue(value);
    }

    private void UpdateMaxValue(float newValue)
    {
        AdjustmentMaxValue = newValue;
    }

    // original Value is only changed by status 
    public void ResetToOriginalValue()
    {
        CurrentValue = MaxValue;
        PreviousValue = MaxValue;
    }

    // Adjustment Value is only used on the Battle
    public void ResetToAdjustmentValue()
    {
        CurrentValue = AdjustmentMaxValue;
        PreviousValue = AdjustmentMaxValue;
    }
}
