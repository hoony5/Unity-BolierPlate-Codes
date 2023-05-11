using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PetCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreatePetAbility CreatePetAbility { get; private set; }
    [field:SerializeField] public CreatePetTraits CreatePetTraits { get; private set; }
    private Dictionary<string, Pet> petDataDictionary;

    public Pet GetPetData(string petName)
    {
        return petDataDictionary.TryGetValue(petName, out Pet pet) ? pet : null;
    }

    public void ClearPetData()
    {
        petDataDictionary.Clear();
    }
    
    public void Init()
    {
        List<Pet> pets = CreatePetAbility.SetPets();
        CreatePetTraits.SetPetAttributes(ref pets);
        petDataDictionary = pets.ToDictionary(key => key.Name, value => value);
    }
}