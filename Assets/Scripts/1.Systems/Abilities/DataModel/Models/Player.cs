
using UnityEngine;

[System.Serializable]
public class Player : Character
{
    [field:SerializeField] public PlayerAttributes PlayerAttributes { get;  set; }
    [field:SerializeField] public bool IsOtherPlayer { get;  set; }
}
