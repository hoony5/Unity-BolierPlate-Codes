using UnityEngine;

[CreateAssetMenu(fileName = "new NormalEffect", menuName = "ScriptableObject/Battle/NormalEffect", order = 0)]
public class NormalEffect : ScriptableObject
{
    [SerializeField] private CalculationType calculationType;
    [SerializeField] private ApplyTargetType applyTargetType;
    [SerializeField] private float adustment;
    
    public float Adustment => adustment;
    public CalculationType CalculationType => calculationType;
    public ApplyTargetType ApplyTargetType => applyTargetType;
}