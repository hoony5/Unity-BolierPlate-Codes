using System.Collections;
using System.Collections.Generic;
using ArtificerPro.Stats;
using UnityEngine;

namespace ArtificerPro.Demo
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Stat speed;
        
        private Rigidbody2D _rbody;
        
        private void Start()
        {
            _rbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            _rbody.velocity = new Vector2(h, v).normalized * speed.GetValue(gameObject);
        }
    }
}
