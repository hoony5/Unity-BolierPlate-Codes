using System.Collections.Generic;
using ArtificerPro.Item;
using UnityEngine;
using WayfarerGames.Common;

namespace ArtificerPro.LootTable
{
    [CreateAssetMenu(menuName = "Artificer Pro/Loot Table")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] private List<BaseItem> loot;

        public BaseItem Get()
        {
            return Squirrel3.Instance.WeightedRandom(loot);
        }
    }
}