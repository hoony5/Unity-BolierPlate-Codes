using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBleedingEffect", menuName = "ScriptableObject/Effects/Battle/Bleeding")]
public class BleedingEffect : ScriptableObject, IDurationEffect
{
    [SerializeField] private float tickInterval = 1f;
    [SerializeField] private string effectName = "Bleeding"; // Name of the effect
    [NonSerialized] private float damagePerTick = 10f;
    [NonSerialized] private int effectIndex = -1;

    public string Name => effectName;

    public void SetDamage(float damage)
    {
        damagePerTick = damage;
    }

    public void SetEffectIndex(int index)
    {
        effectIndex = index;
    }

    [ToDo("After creating Formula System, then Update Apply Damage Method")]
    public void UpdateEffect(CharacterBehaviour character, float deltaTime)
    {
        if (effectIndex < 0)
        {
            Debug.LogWarning("BleedingEffect is not properly initialized");
            return;
        }

        // Calculate the total number of ticks based on the duration and tick interval.
        int numTicks = Mathf.CeilToInt(deltaTime / tickInterval);

        // Apply damage for each tick.
        for (int i = 0; i < numTicks; i++)
        {
            //character.OriginalStat.GetStatuses()["hp"].Value -= damagePerTick;
        }
    }
}
