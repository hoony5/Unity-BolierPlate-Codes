using System;
using ArtificerPro.Events;
using UnityEngine;
using UnityEngine.Pool;
using WayfarerGames.Common;

namespace ArtificerPro.Demo
{
    public class SpawnNewOnEnemyDie : MonoBehaviour
    {
        [SerializeField] private TriggerItemEvent onDie;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int maxPoolSize;

        [SerializeField] private float radius;

        private int _current;
        
        private IObjectPool<GameObject> _pool;

        private IObjectPool<GameObject> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, 10, maxPoolSize);
                
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
            var go = Instantiate(enemyPrefab);
            go.name = $"Pooled {enemyPrefab.name} ({name})";
            var pooledItem = go.GetInterface<IPooledItem<GameObject>>();
            if (pooledItem == null)
            {
                Destroy(go);
                throw new Exception($"Failed to create pooled item for {gameObject.name}. Pooled item prefab must have a component that implements the IPooledItem interface");
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
            obj.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// Called when an item is taken from the pool using Get
        /// </summary>
        /// <param name="obj">The GameObject being returned</param>
        private void OnTakeFromPool(GameObject obj)
        {
            obj.gameObject.SetActive(true);
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
        
        private void OnEnable()
        {
            onDie.OnEvent += OnDie;
        }

        private void OnDisable()
        {
            onDie.OnEvent -= OnDie;
        }

        private void Start()
        {
            for (int i = 0; i < maxPoolSize; i++)
                OnDie(new TriggerEventArgs(gameObject, gameObject, 0));
        }

        private void OnDie(TriggerEventArgs _)
        {
            ++_current;
            Vector3 result = UnityEngine.Random.insideUnitCircle;

            var newEnemy = Pool.Get();
            newEnemy.gameObject.name = "Enemy " + _current;
            newEnemy.gameObject.SetActive(true);
            newEnemy.transform.position = transform.position + (result * radius);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}