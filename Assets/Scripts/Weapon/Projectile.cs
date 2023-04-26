using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using Services.Pause;

namespace Game
{
	public class Projectile : BaseProjectile
	{
        private Sequence seq;
        private IObjectPool<Projectile> pool;
        private bool isReleased;

        private void OnEnable()
        {
            isReleased = false;
        }

        public void SetPool(IObjectPool<Projectile> poolToSet)
        {
            pool = poolToSet;
        }

        public override void SetSpeed(float speed)
        {
            transform.localScale = Vector3.one;
            seq = DOTween.Sequence();
            seq.Append(transform.DOMove(transform.position + transform.up * speed * ConstantVariables.BULLET_TIME_TO_DESTROY, ConstantVariables.BULLET_TIME_TO_DESTROY)
            .SetEase(Ease.Linear)
            .OnComplete(DestroyBullet));
            seq.Insert(0, transform.DOScale(ConstantVariables.BulletSizeModifier, ConstantVariables.BulletSizeDuration));
		}

        public void SetSpeedAndDistance(float speed, float distance)
        {
            float scale = Random.Range(ConstantVariables.MinSizeForShotgunBullet, 1);
            transform.localScale = new Vector2(scale, scale);
            seq = DOTween.Sequence();
            seq.Append(transform.DOMove(transform.position + transform.up * speed * distance, distance)
            .SetEase(Ease.Linear)
            .OnComplete(DestroyBullet));
            seq.Insert(0, transform.DOScale(ConstantVariables.BulletSizeModifier * scale, ConstantVariables.BulletSizeDuration));
        }

        protected override void DestroyBullet()
        {
            seq.Kill();
            if (!isReleased)
            {
                pool.Release(this);
                isReleased = true;
            }
        }

        public override void PauseOn()
        {
            seq.Pause();
        }

        public override void PauseOff()
        {
            seq.Play();
        }
    }
}
