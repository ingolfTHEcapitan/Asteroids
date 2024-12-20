using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class ScreenBounds : MonoBehaviour
	{
		private BoxCollider2D _boxCollider2D;
		private Camera _mainCamera;

		private void Awake()
		{
			_boxCollider2D = GetComponent<BoxCollider2D>();
			_boxCollider2D.isTrigger = true;
			_mainCamera = Camera.main;
		}

		private void Start()
		{
			transform.position = Vector3.zero;
			UpdateBoundsSize();
		}

		private void UpdateBoundsSize()
		{
			// orthographicSize равен половине высоты камеры, умножением на два получается высота камеры
			float ySize = _mainCamera.orthographicSize * 2;
			// Ширина камеры равна высоте камеры умноженое на соотношение сторон
			Vector2 boxColiderSize = new Vector2(ySize * _mainCamera.aspect, ySize);
			_boxCollider2D.size = boxColiderSize;
		}

		void OnTriggerExit2D(Collider2D collider)
		{
			GameEvents.OnExitTriggerFired(collider);          
		}

		
	}
}


