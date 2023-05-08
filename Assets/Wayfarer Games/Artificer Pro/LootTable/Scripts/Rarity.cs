using UnityEngine;

namespace ArtificerPro.LootTable
{
    [CreateAssetMenu(menuName = "Artificer Pro/Rarity")]
    public class Rarity : ScriptableObject
    {
        [SerializeField] private Color color = Color.white;
        public Color Color => color;

        [SerializeField] private float weight;
        public float Weight => weight;
    }
}