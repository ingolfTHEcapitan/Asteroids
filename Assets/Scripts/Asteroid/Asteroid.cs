using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Asteroid : MonoBehaviour
	{
		[SerializeField] private Sprite[] _asteroids;
		[SerializeField] private float _minSize = 0.5f;
		[SerializeField] private float _maxSize = 1.5f;
		[SerializeField] private float _speed = 50;
		private float _currentSize;
		private SpriteRenderer _spriteRenderer;
		private Rigidbody2D _rigidbody2D;
		

		void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}
		
		private void Start()
		{
			_currentSize = Random.Range(_minSize, _maxSize);
			
			_spriteRenderer.sprite = _asteroids[Random.Range(0, _asteroids.Length)];
			transform.eulerAngles = new Vector3(0, 0, Random.value * 360.0f);
			transform.localScale = Vector3.one * _currentSize;
			_rigidbody2D.mass = _currentSize;
		}
		
		public void SetTrajectory(Vector3 direction)
		{
			_rigidbody2D.AddForce(direction * _speed);
		}

		
	}
}