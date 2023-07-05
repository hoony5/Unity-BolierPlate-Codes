using UnityEngine;

[System.Serializable]
public class HealthKitDataValidationCheker : MonoBehaviour
{
    [ToDo("Convert HealthKitData Secondary Data, Formula is in the document, Excel")]
    public PlayerHealthKitData ConvertHealthKitData()
    {
        throw new System.NotImplementedException();
    }

    [ToDo("Consider the WorkoutTime and HeartRate")]
    public double IsValidateExerciseData(PlayerHealthKitData data)
    {
        throw new System.NotImplementedException();
    }

    [ToDo("Compare criteria with the Player's HeartRate Average")]
    public bool IsValidateHeartRate(PlayerHealthKitData data, double criteria)
    {
        throw new System.NotImplementedException();
    }
    
    [ToDo("Compare criteria with the Player's Steps")]
    public bool IsValidateSteps(PlayerHealthKitData data, double criteria)
    {
        throw new System.NotImplementedException();
    }
    
    [ToDo("Compare criteria with the Player's WorkoutTime")]
    public bool IsValidateWorkoutTime(PlayerHealthKitData data, double criteria)
    {
        throw new System.NotImplementedException();
    }
    
    [ToDo("Compare criteria with the Player's BurnedCalories")]
    public bool IsValidateBurnedCalories(PlayerHealthKitData data, double criteria)
    {
        throw new System.NotImplementedException();
    }
    
    [ToDo("Compare criteria with the Player's Distance")]
    public bool IsValidateDistance(PlayerHealthKitData data, double criteria)
    {
        throw new System.NotImplementedException();
    }
    
}
