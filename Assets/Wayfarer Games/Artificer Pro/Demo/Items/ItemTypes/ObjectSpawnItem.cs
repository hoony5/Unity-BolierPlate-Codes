using System.Collections.Generic;
using ArtificerPro.Events;
using ArtificerPro.Item;
using UnityEngine;
using UnityEngine.Pool;
using WayfarerGames.Common;

namespace ArtificerPro.Demo.Items.ItemTypes
{
    [CreateAssetMenu(fileName = "Item", menuName = "Artificer Pro/Create Object Spawn Item")]
    public class ObjectSpawnItem : BaseItem
    {
        [SerializeField, Tooltip("The item to spawn on equip")] 
        private GameObject itemPrefab;
        
        [SerializeField, Tooltip("Destroy any objects as soon as this item is unequipped")] 
        private bool destroyObjectsOnUnequip = true;
        
        [SerializeField, Tooltip("The maximum number of objects that can be created by this item")] 
        private int maxPoolSize = 10;
        
        [SerializeField, Tooltip("Throw errors if we try to release an item that is already in the pool")] 
        private bool collectionChecks = true;

        private IObjectPool<GameObject> _pool;
        private readonly Stack<GameObject> _activeItems = new();

        private IObjectPool<GameObject> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                
                return _pool;
            }
        }

        #region PoolInitialization
        /// <summary>
        /// Create items for the pool.
        /// </summary>
        /// <returns>The newly created pooled item</returns>
        private GameObject CreatePooledItem()
        {
            var go = Instantiate(itemPrefab);
            go.name = $"Pooled {itemPrefab.name} ({name})";
            var pooledItem = go.GetInterface<IPooledItem<GameObject>>();
            if (pooledItem == null)
            {
                Destroy(go);
                throw new System.Exception($"Failed to create pooled item for {itemPrefab.name}. Pooled item prefab must have a component that implements the IPooledItem interface");
            }
            
            pooledItem.SetPool(Pool);
            return go;
        }
        
        /// <summary>
        /// Called when an item is returned to the pool using Release
        /// </summary>
        /// <param name="obj">The GameObject that was released</param>
        private void OnReturnedToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
        
        /// <summary>
        /// Called when an item is taken from the pool using Get
        /// </summary>
        /// <param name="obj">The GameObject being returned</param>
        private void OnTakeFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        /// <summary>
        /// If the pool capacity is reached then any items returned will be destroyed.
        /// We can control what the destroy behavior does, here we destroy the GameObject.
        /// </summary>
        /// <param name="obj">The GameObject to be destroyed</param>
        private void OnDestroyPoolObject(GameObject obj)
        {
            Destroy(obj);
        }
        #endregion

        public override void UnEquipItem()
        {
            base.UnEquipItem();
            
            if (destroyObjectsOnUnequip)
                while (_activeItems.Count > 0)
                    Pool.Release(_activeItems.Pop());
        }

        public override void DoEffect(TriggerEventArgs args)
        {
            var createdObject = Pool.Get();
            createdObject.transform.position = args.Target.transform.position;
            _activeItems.Push(createdObject);
        }
    }
}