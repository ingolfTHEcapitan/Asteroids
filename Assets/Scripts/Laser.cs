using UnityEngine;

namespace Asteroids
{
	public class Laser : MonoBehaviour
	{
		[SerializeField] private float _speed = 500f;
		private Rigidbody2D _rigidbody2D;
		
		private void Awake() 
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}
		
		public void Project(Vector2 direction)
		{
			_rigidbody2D.velocity = direction * _speed;
		}
		
		private void OnTriggerExit2D(Collider2D other) 
		{
			Destroy(gameObject);
		}
		
		private void OnCollisionEnter2D(Collision2D other) 
		{
			Destroy(gameObject);
		}
	}
}