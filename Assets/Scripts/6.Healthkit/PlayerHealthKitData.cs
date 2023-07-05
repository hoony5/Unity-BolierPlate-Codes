using System;
using UnityEngine;

[Serializable]
public class PlayerHealthKitData
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public bool UseWheelChair { get; private set; }
    [field: SerializeField] public double Steps { get; private set; }
    [field: SerializeField] public double Distance { get; private set; }
    [field: SerializeField] public double HeartRateAverage { get; private set; }
    [field: SerializeField] public double WorkoutTime { get; private set; }
    [field: SerializeField] public double BurnedCalories { get; private set; }
    
    public PlayerHealthKitData(string name , bool useWheelChair, double steps, double distance, double heartRateAverage, double workoutTime, double burnedCalories)
    {
        Name = name;
        UseWheelChair = useWheelChair;
        Steps = steps;
        Distance = distance;
        HeartRateAverage = heartRateAverage;
        WorkoutTime = workoutTime;
        BurnedCalories = burnedCalories;
    }
}
