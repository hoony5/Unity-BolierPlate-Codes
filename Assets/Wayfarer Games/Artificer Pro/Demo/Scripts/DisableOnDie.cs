using ArtificerPro.Events;
using UnityEngine;

namespace ArtificerPro.Demo
{
    public class DisableOnDie : MonoBehaviour
    {
        [SerializeField] private TriggerItemEvent onDie;
        
        private void OnEnable()
        {
            onDie.OnEvent += OnDie;
        }

        private void OnDisable()
        {
            onDie.OnEvent -= OnDie;
        }

        private void OnDie(TriggerEventArgs obj)
        {
            obj.Target.SetActive(false);
        }
    }
}