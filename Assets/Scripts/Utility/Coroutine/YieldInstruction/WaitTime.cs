using System;
using UnityEngine;

public class WaitForTime : CustomYieldInstruction
{
    // Define TicksPerSecond constant for calculating end time
    private const long TicksPerSecond = TimeSpan.TicksPerSecond;

    // Define seconds to wait
    private readonly float _seconds;

    // Define the end time to wait for
    private long _endTime;

    // Constructor for WaitForTime
    public WaitForTime(float seconds)
    {
        // Ensure that seconds is greater than 0
        _seconds = Mathf.Max(0.0001f, seconds);

        // Calculate and set the end time to wait for
        UpdateEndTime();
    }

    // Method for updating the end time
    private void UpdateEndTime()
    {
        // Calculate the end time using the current system time and TicksPerSecond
        _endTime = DateTime.Now.Ticks + (long)(TicksPerSecond * _seconds);
    }

    // Method for resetting the wait time
    public void Reset()
    {
        // Update the end time to wait for
        UpdateEndTime();
    }

    // Override the keepWaiting property to determine if the wait is complete
    public override bool keepWaiting => DateTime.Now.Ticks < _endTime;
}