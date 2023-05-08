using UnityEngine;

namespace ArtificerPro.Demo
{
    public class PlayerRotateMouse : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            var mouse = Input.mousePosition;
            mouse.z = 1;
            
            var pos = _camera.ScreenToWorldPoint(mouse);
            pos.z = transform.position.z;
            transform.up = pos - transform.position;
        }
    }
}