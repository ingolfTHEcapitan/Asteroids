using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(Animator))]
	public class PlayerVisual : MonoBehaviour
	{
		[SerializeField] private float blinkInterval = 0.25f; // Интервал мигания в секундах
		[SerializeField] private float blinkDuration = 3f;   // Продолжительность мигания в секундах

		private Animator _animator;
		private SpriteRenderer _spriteRenderer;
		
		void Awake()
		{
			_animator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();	
		}

		void OnEnable()
		{
			GameEvents.PlayerRespawned += OnPlayerRespawned;
			GameEvents.PlayerDied += OnPlayerDied;
		}

		void Update()
		{
			_animator.SetFloat("Speed", GameInput.Instance.MovementInput.magnitude);
		}
		
		private void OnPlayerRespawned()
		{
			Player.Instance.gameObject.layer = LayerMask.NameToLayer("IgnoreAsteroidCollision");
			_animator.SetTrigger("Explosion");
		}
		
		// Проверка под конец анимации взрыва
		public void OnPlayerExplosion()
		{
			if (Player.Instance.CurrentLives <= 0)
				GameEvents.OnPlayerDied();
			else
				StartCoroutine(BkinlingRoutine());
		}
		
		private void OnPlayerDied()
		{
			GameInput.PlayerInput.SwitchCurrentActionMap("Keyboard");
			Player.Instance.gameObject.layer = LayerMask.NameToLayer("Player");
		}
		
		public IEnumerator BkinlingRoutine()
		{
			
			Player.Instance.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
			GameInput.PlayerInput.SwitchCurrentActionMap("Keyboard");
			
			float endTime = Time.time + blinkDuration;
			while (Time.time < endTime)
			{
				// Переключаем видимость спрайта
				_spriteRenderer.enabled = !_spriteRenderer.enabled;

				// Ждем указанный интервал
				yield return new WaitForSeconds(blinkInterval);
			}

			// Убедимся, что спрайт видим по окончании мигания
			_spriteRenderer.enabled = true;
			Player.Instance.gameObject.layer = LayerMask.NameToLayer("Player");
		}
	}
}
