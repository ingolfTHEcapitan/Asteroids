using UnityEngine;

namespace Asteroids
{
	public class Laser : MonoBehaviour
	{
		[SerializeField] private float _speed = 500f;
		[SerializeField] private float _lifeTime = 5;
		
		private Rigidbody2D _rigidbody2D;

		private void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		public void Project(Vector2 direction)
		{
			_rigidbody2D.AddForce(direction * _speed);
			
			Destroy(gameObject, _lifeTime);
		}
		
		void OnCollisionExit2D(Collision2D other)
		{
			Destroy(gameObject);
		}
		void OnCollisionEnter(Collision other)
		{
			Destroy(gameObject);
		}
	}
}