using System;
using System.Collections;
using ArtificerPro.Demo.Scripts;
using ArtificerPro.Events;
using UnityEngine;
using UnityEngine.Pool;
using WayfarerGames.Common;

namespace ArtificerPro.Demo
{
    public class Bullet : MonoBehaviour, IPooledItem<Bullet>
    {
        // bullet params
        [SerializeField] private float damage = 1f;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifetime = 1f;

        // event to trigger on bullet died
        [SerializeField] private TriggerItemEvent bulletDieEvent;
        
        // whether or not to release the bullet back to the pool
        // useful for persisting bullets after they die
        [NonSerialized] public bool DontDie;

        // action that runs when the bullet is released back to the pool
        public event Action<Bullet> OnReleased;
        
        private Rigidbody2D _rbody;
        private Collider2D _previousCollider;
        
        // cached WaitForSeconds so we don't allocate
        private WaitForSeconds _wait;
        
        private GameObject _sender;
        public GameObject Sender => _sender;
        
        // the current object pool
        private IObjectPool<Bullet> _pool;

        private Coroutine _disable;

        private static int _idx;
        private int _id;
        public int ID => _id;


        /// <summary>
        /// Initialise the bullet.
        /// </summary>
        private void Start()
        {
            _rbody = GetComponent<Rigidbody2D>();
            _wait = new WaitForSeconds(lifetime);
        }

        /// <summary>
        /// When the bullet is enabled
        /// </summary>
        private void OnEnable()
        {
            // store an ID for the bullet
            _id = ++_idx;
            // clear the previous collider
            _previousCollider = null;
        }

        /// <summary>
        /// Shoot the bullet
        /// </summary>
        /// <param name="sender">The object that fired the bullet</param>
        public void Fire(GameObject sender)
        {
            // if we don't have a rigidbody, this is being called before Start
            // call Start
            if (_rbody == null)
                Start();
            
            // make the bullet move
            _rbody.velocity = transform.up * speed;
            
            // if we've got a running coroutine, cancel it
            if (_disable != null)
                StopCoroutine(_disable);
            
            // disable the bullet after a period of time
            _disable = StartCoroutine(Disable());
            
            // cache the sender
            _sender = sender;
        }

        /// <summary>
        /// When a collision happens
        /// </summary>
        /// <param name="other">The thing we collided with</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            // if we just hit something, ignore collisions with that object
            if (_previousCollider == other) return;
            // store the previous collider so we only collide with it once
            _previousCollider = other;
            
            // damage the other object
            if (!other.TryGetComponent<EntityHealth>(out var health)) return;
            health.Hit(new DamagePacket(_sender, damage));
            
            // release the bullet back to the pool
            Release(other.gameObject);
        }

        /// <summary>
        /// Wait and disable the bullet
        /// </summary>
        /// <returns></returns>
        private IEnumerator Disable()
        {
            yield return _wait;
            // if we've waited this long and we haven't died, we want the bullet to die
            DontDie = false;
            Release(gameObject);
        }
        
        /// <summary>
        /// IPooledItem implementation
        /// </summary>
        /// <param name="pool"></param>
        public void SetPool(IObjectPool<Bullet> pool)
        {
            // cache the object pool
            _pool = pool;
        }

        /// <summary>
        /// IPooledItem implementation
        /// Release the bullet back to the pool
        /// </summary>
        public void Release()
        {
            // if we don't want to release the bullet, just return immediately
            if (DontDie) return;
            
            // release the bullet back to the pool
            _pool.Release(this);
            // broadcast an event to let anything that cares know that the bullet has been released
            OnReleased?.Invoke(this);
        }

        /// <summary>
        /// Release the bullet and trigger the bullet died event
        /// </summary>
        /// <param name="target"></param>
        private void Release(GameObject target)
        {
            // trigger the bullet died event
            // pass through the sender, the target, and the information about the bullet
            bulletDieEvent.Invoke(new TriggerEventArgs(_sender, target, new BulletDiedParams (transform.position, target != gameObject, this)));
            Release();
        }
    }
}