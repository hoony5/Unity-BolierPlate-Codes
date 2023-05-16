using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PetCreateManager : MonoBehaviour
{
    [field:SerializeField] public CreatePetAbility CreatePetAbility { get; private set; }
    [field:SerializeField] public CreatePetTraits CreatePetTraits { get; private set; }
    private Dictionary<string, Pet> petDataDictionary = new Dictionary<string, Pet>();
    private Dictionary<string, GrowablePet> growablePetsDictionary = new Dictionary<string, GrowablePet>();
    private Dictionary<string, EnhancablePet> enhancablePetsDictionary = new Dictionary<string, EnhancablePet>();
    private Dictionary<string, CombinablePet> combinablePetsDictionary = new Dictionary<string, CombinablePet>();
    public Pet GetPetData(string petName)
    {
        return petDataDictionary.TryGetValue(petName, out Pet pet) ? pet : null;
    }
    public GrowablePet GetGrowablePetData(string petName)
    {
        return growablePetsDictionary.TryGetValue(petName, out GrowablePet pet) ? pet : null;
    }
    public EnhancablePet GetEnhancablePetData(string petName)
    {
        return enhancablePetsDictionary.TryGetValue(petName, out EnhancablePet pet) ? pet : null;
    }
    public CombinablePet GetCombinablePetData(string petName)
    {
        return combinablePetsDictionary.TryGetValue(petName, out CombinablePet pet) ? pet : null;
    }

    public void ClearPetData()
    {
        petDataDictionary.Clear();
        growablePetsDictionary.Clear();
        enhancablePetsDictionary.Clear();
        combinablePetsDictionary.Clear();
    }
    
    public void Init()
    {
        List<Pet> pets = CreatePetAbility.GetPets();
        if (pets.Count == 0) return;
        CreatePetTraits.SetPetAttributes(ref pets);
        petDataDictionary = pets.ToDictionary(key => key.Name, value => value);
    }
    public void InitGrowablePets()
    {
        List<GrowablePet> pets = CreatePetAbility.GetGrowablePet();
        if (pets.Count == 0) return;
        CreatePetTraits.SetGrowInfo(ref pets);
        growablePetsDictionary = pets.ToDictionary(key => key.Name, value => value);
    }
    public void InitEnhancablePets()
    {
        List<EnhancablePet> pets = CreatePetAbility.GetEnhancablePet();
        if (pets.Count == 0) return;
        CreatePetTraits.SetEnhanceInfo(ref pets);
        enhancablePetsDictionary = pets.ToDictionary(key => key.Name, value => value);
    }
    public void InitCombinablePets()
    {
        List<CombinablePet> pets = CreatePetAbility.GetCombinablePet();
        if (pets.Count == 0) return;
        combinablePetsDictionary = pets.ToDictionary(key => key.Name, value => value);
    }
}