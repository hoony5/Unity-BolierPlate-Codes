using System;
using ArtificerPro.Events;
using ArtificerPro.Stats;
using UnityEngine;

namespace ArtificerPro.Demo.Scripts
{
    public class DamagePacket
    {
        public GameObject Sender;
        public float Damage;
        
        public DamagePacket(GameObject sender, float damage)
        {
            Sender = sender;
            Damage = damage;
        }
    }
    
    public class EntityHealth : MonoBehaviour
    {
        [SerializeField] private Stat maxHealth;
        [SerializeField] private bool updateCurrentHealthWithMaxHealthChanges;
        public float MaxHealth => maxHealth.GetValue(gameObject);

        private float _currentHealth;
        public float CurrentHealth => _currentHealth;

        [SerializeField] private TriggerItemEvent triggerHealthEvent;
        [SerializeField] private TriggerItemEvent triggerDeathEvent;

        private void OnEnable()
        {
            _currentHealth = MaxHealth;
            maxHealth.OnUpdated += MaxHealthUpdated;
        }

        private void OnDisable()
        {
            maxHealth.OnUpdated -= MaxHealthUpdated;
        }

        private void MaxHealthUpdated(float newVal, float oldVal)
        {
            if (updateCurrentHealthWithMaxHealthChanges)
                _currentHealth += newVal - oldVal;
            Debug.Log($"Max health: {newVal}, Current health: {CurrentHealth}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Hit(new DamagePacket(gameObject, 0.5f));
        }

        public void Hit(DamagePacket damage)
        {
            _currentHealth -= damage.Damage;
            triggerHealthEvent.Invoke(new TriggerEventArgs (damage.Sender, gameObject, damage.Damage));

            if (_currentHealth <= 0)
                triggerDeathEvent.Invoke(new TriggerEventArgs (damage.Sender, gameObject, damage.Damage));
            
            if (_currentHealth >= MaxHealth)
                _currentHealth = MaxHealth;
        }
    }
}