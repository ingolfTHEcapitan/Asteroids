using System.Collections.Generic;
using Asteroids.ScreenWrapping;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(PolygonCollider2D))]
	public class Asteroid : MonoBehaviour, IScreenWrappable
	{
		[SerializeField] private Sprite[] _asteroidsSprites;
		[SerializeField] private float _minSize = 0.5f;
		[SerializeField] private float _maxSize = 1.5f;
		[SerializeField] private float _speed = 50;
		
		private SpriteRenderer _spriteRenderer;
		private Rigidbody2D _rigidbody2D;
		private Animator _animator;
		private PolygonCollider2D _polygonCollider2D;
		private bool _isDestroyed = false;
		
		public static readonly List<Asteroid> Asteroids = new List<Asteroid>();
		public float Size { get; private set; } = 1.0f;
		public float MaxSize { get => _maxSize; private set => _maxSize = value; }

		
		private void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();
			_polygonCollider2D = GetComponent<PolygonCollider2D>();
			
			// Отключаем аниматор что бы не перезаписать спрайты астеройда пока нам не понадобится анимация
			_animator.enabled = false;
			Asteroids.Add(this);
		}

		private void OnEnable()
		{
			GameEvents.PlayerDied += NewGame;
			GameEvents.AsteroidExploded += RemoveAsteroid;
		}

		private static void RemoveAsteroid(Asteroid asteroid)
		{
			Asteroids.Remove(asteroid);
		}
		
		private void Start()
		{
			_spriteRenderer.sprite = _asteroidsSprites[Random.Range(0, _asteroidsSprites.Length)];
			UpdateColliderShape();
			
			transform.eulerAngles = new Vector3(0, 0, Random.value * 360.0f);
			transform.localScale = Vector3.one * Size;
			_rigidbody2D.mass = Size;
		}
		
		public void ParentInitialize(Vector3 direction)
		{
			Size = Random.Range(_minSize, MaxSize);
			SetTrajectory(direction);
		}
		
		public void ChildInitialize(Vector3 direction, Asteroid parentAsteroid)
		{
			Size = parentAsteroid.Size * 0.5f;
			SetTrajectory(direction * _speed);
		}
		
		public void DestroyAsteroid()
		{
			Destroy(gameObject);
		}
		
		private void UpdateColliderShape()
		{
			List<Vector2> physicsShape = new List<Vector2>();
			_spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);  // Получаем первую форму (если их несколько)
			_polygonCollider2D.SetPath(0, physicsShape);  // Применяем форму к коллайдеру
		}
		
		private void SetTrajectory(Vector3 direction)
		{
			_rigidbody2D.AddForce(direction * _speed);
		}
		
        private void OnCollisionEnter2D(Collision2D collision)
		{
			if (!_isDestroyed && collision.gameObject.GetComponent<Laser>() != null)
			{
				if ((Size * 0.5) >= _minSize)
				{
					GameEvents.OnAsteroidSplitted(this);
				}
				
				// Переключаем коллайдер в режим тригера, что бы избежать столкновений
				_polygonCollider2D.isTrigger = true;
				
				_isDestroyed = true;
				// Включаем аниматор что бы проиграть анимацию взрыва
				_animator.enabled = true;
				_animator.Play("AsteroidExplosion");
				GameEvents.OnAsteroidExploded(this);
			}
		}
		
		private static void NewGame()
		{
			foreach (var asteroid in Asteroids)
				Destroy(asteroid.gameObject);
			
			Asteroids.Clear();
		}
	}
}