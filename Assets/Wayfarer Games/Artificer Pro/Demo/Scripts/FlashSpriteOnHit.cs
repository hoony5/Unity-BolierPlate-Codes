using System;
using System.Collections;
using System.Collections.Generic;
using ArtificerPro.Events;
using UnityEngine;

namespace ArtificerPro.Demo
{
    public class FlashSpriteOnHit : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 0.1f;
        [SerializeField] private TriggerItemEvent onHit;
        [SerializeField] private Color color;

        private List<GameObject> _currentlyFlashing = new();
        
        private void OnEnable()
        {
            onHit.OnEvent += OnHit;
        }

        private void OnDisable()
        {
            onHit.OnEvent -= OnHit;
        }

        private void OnHit(TriggerEventArgs obj)
        {
            var sr = obj.Target.GetComponentInChildren<SpriteRenderer>();
            
            StartCoroutine(Flash(sr, sr.color));
        }

        private IEnumerator Flash(SpriteRenderer sr, Color originalColor)
        {
            if (_currentlyFlashing.Contains(sr.gameObject)) yield break;
            _currentlyFlashing.Add(sr.gameObject);
            sr.color = color;

            yield return null;
            var time = fadeTime;

            while (time > 0)
            {
                time -= Time.deltaTime;
                sr.color = Color.Lerp(originalColor, color, time / fadeTime);
                yield return null;
            }

            _currentlyFlashing.Remove(sr.gameObject);
        }
    }
}
