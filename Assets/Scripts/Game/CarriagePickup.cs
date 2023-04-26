using UnityEngine;

namespace Game.Player
{
	public class CarriagePickup : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer spriteRenderer;

		public void Construct(Sprite sprite)
        {
			spriteRenderer.sprite = sprite;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.TryGetComponent<CarriageManager>(out CarriageManager carriageManager))
			{
				carriageManager.AddCarriage(spriteRenderer.sprite);
				Destroy(gameObject);
			}
		}
	}
}
