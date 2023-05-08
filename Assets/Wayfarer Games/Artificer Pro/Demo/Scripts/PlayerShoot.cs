using System;
using System.Collections.Generic;
using ArtificerPro.Stats;
using UnityEngine;
using UnityEngine.Pool;
using WayfarerGames.Common;

namespace ArtificerPro.Demo
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField] private Stat fireRate;
        [SerializeField] private Bullet bulletPrefab;
        
        [SerializeField] private int maxPoolSize;
        
        private float _lastFireTime;
        
        private IObjectPool<Bullet> _pool;

        private IObjectPool<Bullet> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<Bullet>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, maxPoolSize);
                
                return _pool;
            }
        }

        #region PoolInitialization
        /// <summary>
        /// Create items for the pool.
        /// </summary>
        /// <returns>The newly created pooled item</returns>
        private Bullet CreatePooledItem()
        {
            var go = Instantiate(bulletPrefab);
            go.gameObject.name = $"Pooled {bulletPrefab.name} ({name})";
            var pooledItem = go.gameObject.GetInterface<IPooledItem<Bullet>>();
            if (pooledItem == null)
            {
                Destroy(go);
                throw new Exception($"Failed to create pooled item for {bulletPrefab.name}. Pooled item prefab must have a component that implements the IPooledItem interface");
            }
            
            pooledItem.SetPool(Pool);
            return go;
        }
        
        /// <summary>
        /// Called when an item is returned to the pool using Release
        /// </summary>
        /// <param name="obj">The GameObject that was released</param>
        private void OnReturnedToPool(Bullet obj)
        {
            obj.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Called when an item is taken from the pool using Get
        /// </summary>
        /// <param name="obj">The GameObject being returned</param>
        private void OnTakeFromPool(Bullet obj)
        {
            obj.gameObject.SetActive(true);
        }

        /// <summary>
        /// If the pool capacity is reached then any items returned will be destroyed.
        /// We can control what the destroy behavior does, here we destroy the GameObject.
        /// </summary>
        /// <param name="obj">The GameObject to be destroyed</param>
        private void OnDestroyPoolObject(Bullet obj)
        {
            Destroy(obj);
        }
        #endregion

        private void Update()
        {
            if (Input.GetMouseButton(0) && _lastFireTime + fireRate.GetValue(gameObject) < Time.time)
            {
                _lastFireTime = Time.time;
                
                var bullet = Pool.Get();
                bullet.transform.up = transform.up;
                bullet.transform.position = transform.position;
                bullet.Fire(gameObject);
            }
        }
    }
}
