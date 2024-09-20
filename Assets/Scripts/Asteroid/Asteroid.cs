using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Asteroid : MonoBehaviour, IScreenWrappable
	{
		[SerializeField] private Sprite[] _asteroids;
		[SerializeField] private float _minSize = 0.5f;
		[SerializeField] private float _maxSize = 1.5f;
		[SerializeField] private float _speed = 50;
		
		private SpriteRenderer _spriteRenderer;
		private Rigidbody2D _rigidbody2D;
		private Animator _animator;
		private PolygonCollider2D _polygonCollider2D;
		private float _size = 1.0f;
		private bool _isDestroyed = false;

		public float Size { get => _size; set => _size = value; }
		public float MaxSize { get => _maxSize; private set => _maxSize = value; }
		public float MinSize { get => _minSize; private set => _minSize = value; }

		void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();
			_polygonCollider2D = GetComponent<PolygonCollider2D>();
			
			// Отключаем аниматор что бы не перезаписать спрайты астеройда пока нам не понадобится анимация
			_animator.enabled = false;
		}

		private void OnEnable() => GameEvents.PlayerDied += NewGame;

		private void Start()
		{
			_spriteRenderer.sprite = _asteroids[Random.Range(0, _asteroids.Length)];
			UpdateColliderShape();
			
			transform.eulerAngles = new Vector3(0, 0, Random.value * 360.0f);
			transform.localScale = Vector3.one * Size;
			_rigidbody2D.mass = Size;
		}
		
		public void UpdateColliderShape()
		{
			List<Vector2> physicsShape = new List<Vector2>();
			_spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);  // Получаем первую форму (если их несколько)
			_polygonCollider2D.SetPath(0, physicsShape);  // Применяем форму к коллайдеру
		}
		
		public void SetTrajectory(Vector3 direction)
		{
			_rigidbody2D.AddForce(direction * _speed);
		}

		void OnCollisionEnter2D(Collision2D collision)
		{

			if (!_isDestroyed && collision.gameObject.GetComponent<Laser>() != null)
			{
				if ((Size * 0.5) >= _minSize)
				{
					SplitAsteroid();
					SplitAsteroid();
				}
				
				// Переключаем коладер в редим тригера что бы избежать столкновений
				_polygonCollider2D.isTrigger = true;
				
				_isDestroyed = true;
				// Включаем аниматор что бы проиграть анимацию взрыва
				_animator.enabled = true;
				_animator.Play("AsteroidExplosion");
				GameEvents.OnAsteroidExploded(this);
			}
		}
		
		private void SplitAsteroid()
		{
			Vector2 position = transform.position;
			position += Random.insideUnitCircle * 0.5f;
			
			Asteroid halfAsteroid = Instantiate(this, position, transform.rotation);
			halfAsteroid.Size = Size * 0.5f;
			
			halfAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * _speed);
		}	
		
		public void DestroyAsteroid()
		{
			Destroy(gameObject);
		}
		
		private static void NewGame()
		{
			List<Asteroid> asteroids = new List<Asteroid>(FindObjectsOfType<Asteroid>());

			foreach (Asteroid asteroid in asteroids)
				Destroy(asteroid.gameObject);
		}
	}
}