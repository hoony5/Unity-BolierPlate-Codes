using UnityEngine;

[System.Serializable]
public class NPCReferenceInfo
{
    [field:SerializeField] public string Name { get; set; }
    [field:SerializeField] public Sprite NormalProfile { get; set; }
    [field:SerializeField] public Sprite DialogueProfile { get; set; }
}
