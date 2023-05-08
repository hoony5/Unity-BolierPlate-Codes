using UnityEngine;

namespace ArtificerPro.Demo
{
    /// <summary>
    /// Information to pass through when the bullet dies
    /// </summary>
    public class BulletDiedParams
    {
        public Vector3 Position;
        public bool HitSomething;
        public Bullet BulletObject;

        public BulletDiedParams(Vector3 position, bool hitSomething, Bullet bulletObject)
        {
            Position = position;
            HitSomething = hitSomething;
            BulletObject = bulletObject;
        }
    }
}