using System.Collections;
using System.Collections.Generic;
using ArtificerPro.Events;
using ArtificerPro.Item;
using UnityEngine;

namespace ArtificerPro.Demo.Items.ItemTypes
{
    [CreateAssetMenu(fileName = "Item", menuName = "Artificer Pro/Create Bullet Rebound Item")]
    public class BulletReboundItem : BaseItem
    {
        [SerializeField] private float checkRadius = 2f;
        [SerializeField] private int numBounces = 2;
        [SerializeField] private LayerMask layersToCheck;
        
        // store how many bounces have happened, per bullet
        // <int bullet ID, int number of bounces> 
        private Dictionary<int, int> _bounces = new();

        // cached colliders for non-allocated physics functions
        private Collider2D[] _cachedColliders = new Collider2D[5];

        /// <summary>
        /// Trigger the item's effect
        /// </summary>
        /// <param name="args">Any relevant information</param>
        public override void DoEffect(TriggerEventArgs args)
        {
            // if there are no bullet parameters, early return
            // also early return if the bullet didn't hit anything
            if (args.Data is not BulletDiedParams bulletDiedParams || !bulletDiedParams.HitSomething) return;

            // grab the bullet object
            var bullet = bulletDiedParams.BulletObject;
            
            // add the bullet's id to the dictionary if it isn't already there
            if (!_bounces.ContainsKey(bullet.ID))
            {
                _bounces.Add(bullet.ID, 0);
                // also subscribe to the bullet released event
                bullet.OnReleased += BulletReleased;
            }

            // if we've bounced too many times, early return and let the bullet die
            // use the current stacks so we can stack the item for more bounces
            // 0 extra bounces when there's only 1 item, 1 when there are 2 item, etc
            if (_bounces[bullet.ID] >= numBounces + (CurrentStacks - 1))
            {
                bullet.DontDie = false;
                return;
            }

            // increment the bounces counter
            ++_bounces[bullet.ID];
            
            // stop the bullet from releasing back to the pool
            bullet.DontDie = true;

            // find some nearby enemies
            var numFound = Physics2D.OverlapCircleNonAlloc(bullet.transform.position, 
                checkRadius, _cachedColliders, layersToCheck);
            
            // if we didn't find any at all, then just shoot it in a random direction
            if (numFound == 0)
                bullet.transform.up = Quaternion.Euler(0,0,Random.Range(0, 180)) * bullet.transform.up;
            else
            {
                int idx = 0;
                // make sure we don't target the same enemy we just hit
                while (idx < numFound && _cachedColliders[idx].gameObject == args.Target)
                    ++idx;
                
                // if we only found the enemy we just hit, fire in a random direction
                if (idx == numFound)
                    bullet.transform.up = Quaternion.Euler(0,0,Random.Range(0, 180)) * bullet.transform.up;
                else // otherwise, target the bullet
                    bullet.transform.up = _cachedColliders[idx].transform.position - bulletDiedParams.Position;
                
            }
            bullet.Fire(bullet.Sender);
        }

        /// <summary>
        /// When the bullet gets released
        /// </summary>
        /// <param name="bullet">The bullet being released</param>
        private void BulletReleased(Bullet bullet)
        {
            // stop listening to this event
            bullet.OnReleased -= BulletReleased;
            
            // remove the bullet from the dictionary
            if (_bounces.ContainsKey(bullet.ID))
                _bounces.Remove(bullet.ID);
        }
    }
}