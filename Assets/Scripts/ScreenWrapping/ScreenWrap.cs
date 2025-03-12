using Asteroids.ScreenWrapping;
using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class ScreenWrap : MonoBehaviour
	{
		// Смещение при телепортации на другую сторону экрана, что бы оказаться на экране, а не за его пределами
		[SerializeField] private float _teleportOffset = 0.2f;
		[SerializeField] protected float _cornerOffset = 1f;
		
		private BoxCollider2D _boxCollider2D;
		
		private void Awake() 
		{
			_boxCollider2D = GetComponent<BoxCollider2D>();
		}

		private void OnEnable() => GameEvents.ExitTriggerFired += ColliderWrapper;
		private void OnDisable() => GameEvents.ExitTriggerFired -= ColliderWrapper;

		
		private void ColliderWrapper(Collider2D other)
	   	{
			if (other.TryGetComponent<IScreenWrappable>(out var _))
			{
				Vector3 newPosition = CalculateWrappedPosition(other.transform.position);
				other.gameObject.transform.position = newPosition;
			}
	   	}

		private Vector3 CalculateWrappedPosition(Vector3 worldPosition)
		{
			// Находимся ли мы за экраном по ширине или высоте
			// На основе этого можно решить в какую позицию телепортировать игрока или объект.
			bool xBoundsResult = 
				Mathf.Abs(worldPosition.x) > Mathf.Abs(_boxCollider2D.bounds.min.x) - _cornerOffset;
			bool yBoundsResult = 
				Mathf.Abs(worldPosition.y) > Mathf.Abs(_boxCollider2D.bounds.min.y) - _cornerOffset;
			
			Vector2 signWorldPosition = new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));
			
			// Если мы за пределами экрана в углу
			if (xBoundsResult && yBoundsResult)
			{
				// Вычисляем противоположный вектор на другой стороне центра системы координат.
				// Для угла перемножаем вектор нашей позиции на отрицательный вектор, что бы инвертировать позицию
				// И прибавляем вектор смещения.
				
				return Vector2.Scale(worldPosition, Vector2.one * -1) 
					+ Vector2.Scale(new Vector2(_teleportOffset, _teleportOffset), signWorldPosition);
			}
			else if (xBoundsResult)
			{
				return new Vector2(worldPosition.x * -1, worldPosition.y) 
					+ new Vector2(_teleportOffset * worldPosition.x, _teleportOffset);
			}
			else if (yBoundsResult) 
			{
				return new Vector2(worldPosition.x, worldPosition.y * -1) 
					+ new Vector2(_teleportOffset, _teleportOffset * worldPosition.y);
			}
			else
			{
				return worldPosition;
			}	
		}	
		
	}
}