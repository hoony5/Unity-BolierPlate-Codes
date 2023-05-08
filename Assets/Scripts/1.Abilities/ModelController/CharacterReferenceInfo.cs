using UnityEngine;

[System.Serializable]
public class CharacterReferenceInfo
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public Sprite NormalProfile { get; private set; }
    [field:SerializeField] public Sprite DialogueProfile { get; private set; }
}
