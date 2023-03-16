using System;
using UnityEngine;
using UnityEngine.Events;

public class ChangableAbility : MonoBehaviour
{
    [SerializeField] private string _rawName;
    public string RawName => _rawName;
    [SerializeField] private float _previousValue;
    public float PreviousValue => _previousValue;
    [SerializeField] private float _currentValue;
    public float CurrentValue => _currentValue;
    [SerializeField] private float _maxValue;
    public float MaxValue => _maxValue;
    public UnityEvent<float> onChangedValue;

    private void Start()
    {
        InitValue();
    }

    public void InitValue()
    {
        _currentValue = _maxValue;
    }
    
    private void CheckChangedValue()
    {
        if (_currentValue > _maxValue) _currentValue = _maxValue;
        if (_currentValue < 0) _currentValue = 0;

        if (!(Math.Abs(_currentValue - _previousValue) > 0.0001f)) return;
        
        _previousValue = _currentValue;
        onChangedValue?.Invoke(_currentValue);
    }
    public void AddValue(float value)
    {
        _currentValue += value;
        CheckChangedValue();
    }

    public void SetValue(float value)
    {
        _currentValue = value;
        CheckChangedValue();
    }
}