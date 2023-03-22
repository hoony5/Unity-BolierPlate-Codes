using UnityEngine;

[CreateAssetMenu(fileName = "new EnumSO", menuName = "ScriptableObject/Util/EnumSO", order = 0)]
public class ScriptableObjectEnum : ScriptableObject
{
    [SerializeField] private CustomEnum<ScriptableObject> _customEnum;
    public CustomEnum<ScriptableObject> Enum => _customEnum;
}