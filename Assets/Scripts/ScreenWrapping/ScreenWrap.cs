using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class ScreenWrap : MonoBehaviour
	{
		// �������� ��� ������������ �� ����� ������� ������ ��� �� �������� �� ������ � �� �� ��� ���������
		[SerializeField] private float _teleportOffset = 0.2f;
		[SerializeField] protected float _cornerOffset = 1f;
		
		private BoxCollider2D _boxCollider2D;
		
		private void Awake() 
		{
			_boxCollider2D = GetComponent<BoxCollider2D>();
		}

		public void ColiderWrapper(Collider2D collider)
	   	{
			Debug.Log(collider.name);
			Vector3 newPosition = CalculateWrappedPosirion(collider.transform.position);
			collider.gameObject.transform.position = newPosition;
	   	}
		
		public Vector3 CalculateWrappedPosirion(Vector3 worldPosition)
		{
			// ��������� �� �� �� ������� �� ������ ��� ������
			// �� ������ ����� ����� ������ � ����� ������� ��������������� ������ ��� ������.
			bool xBoundsResult = 
				Mathf.Abs(worldPosition.x) > Mathf.Abs(_boxCollider2D.bounds.min.x) - _cornerOffset;
			bool yBoundsResult = 
				Mathf.Abs(worldPosition.y) > Mathf.Abs(_boxCollider2D.bounds.min.y) - _cornerOffset;
			
			Vector2 signWorldPosition = new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));
			
			// ���� �� �� ��������� ������ � ����
			if (xBoundsResult && yBoundsResult)
			{
				// ��������� ��������������� ������ �� ������ ������� ������ ������� ���������.
				// ��� ���� ����������� ������ ����� ������� �� ������������� ������, ��� �� ������������� �������
				// � ���������� ������ ��������.
				
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