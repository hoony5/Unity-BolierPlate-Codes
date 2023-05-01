using System.Collections.Generic;
using UnityEngine;

public class CreateEffectFactory : MonoBehaviour
{
    [Header("Effect Base Info")]
    public OneAbilityResourcesManager oneAbilityResourcesManager;
    public CombinedAbilityResourcesManager combinedAbilityResourcesManager;
    public ComplexAbilityResourcesManager complexAbilityResourcesManager;
    
    [Header("Effect Value Info")]
    public EffectValueInfo  effectValueManager;
    [Header("Effect Ability Info")]
    public EffectAbilityManager  effectAbilityManager;
    [Header("Effect Search Status Info")]
    public EffectSearchStatInfoManager  effectSearchSatInfoManager;



    public void CreateEffectList()
    {
        List<Effect> allOneAbilities = oneAbilityResourcesManager.LoadAllOneAbilities();

       
        foreach (Effect ability in allOneAbilities)
        {

        }
    }
}