using UnityEngine;
using WayfarerGames.Common;

namespace ArtificerPro.Item
{
    [CreateAssetMenu(menuName = "Artificer Pro/Create Item Event", fileName = "ItemEvent")]
    public class BaseItemEvent : ScriptableEvent<BaseItem>
    {
        
    }
}