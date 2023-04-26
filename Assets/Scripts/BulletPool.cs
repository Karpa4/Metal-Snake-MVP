using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Projectile bulletPrefab;

        public IObjectPool<Projectile> Bullets;

        private void Awake()
        {
            Bullets = new ObjectPool<Projectile>(CreateBullet, GetBullet, ReleaseBullet);
        }

        private Projectile CreateBullet()
        {
            Projectile bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetPool(Bullets);
            return bullet;
        }

        private void GetBullet(Projectile bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void ReleaseBullet(Projectile bullet)
        {
            bullet.gameObject.SetActive(false);
        }

    }
}
