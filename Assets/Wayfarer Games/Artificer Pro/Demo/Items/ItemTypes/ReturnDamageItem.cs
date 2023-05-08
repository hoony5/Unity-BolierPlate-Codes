using ArtificerPro.Demo.Scripts;
using ArtificerPro.Events;
using ArtificerPro.Item;
using UnityEngine;

namespace ArtificerPro.Demo.ItemTypes
{
    [CreateAssetMenu(fileName = "Item", menuName = "Artificer Pro/Create Return Damage Item")]
    public class ReturnDamageItem : BaseItem
    {
        [SerializeField, Range(0, 1)] private float damagePercentage;

        public override void DoEffect(TriggerEventArgs args)
        {
            if (args.Data is float damage)
                args.Target.GetComponent<EntityHealth>().Hit(new DamagePacket(args.Sender, damage * damagePercentage));
        }
    }
}