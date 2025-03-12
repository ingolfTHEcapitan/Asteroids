using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
	public class PlayerVisual : MonoBehaviour
	{
		[SerializeField] private float blinkInterval = 0.25f; // Интервал мигания в секундах
		[SerializeField] private float blinkDuration = 3f;   // Продолжительность мигания в секундах

		private Animator _animator;
		private SpriteRenderer _spriteRenderer;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();	
		}

		private void OnEnable()
		{
			GameEvents.PlayerRespawned += OnPlayerRespawned;
			GameEvents.PlayerDied += OnPlayerDied;
		}

		private void OnDisable()
		{
			GameEvents.PlayerRespawned -= OnPlayerRespawned;
			GameEvents.PlayerDied -= OnPlayerDied;
		}

		private void Update()
		{
			_animator.SetFloat("Speed", GameInput.Instance.MovementInput.magnitude);
		}
		
		public void OnPlayerExplosion()
		{
			if (Player.Instance.CurrentHealth <= 0)
				GameEvents.OnPlayerDied();
			else
				StartCoroutine(BlinkingRoutine());
		}
		
		private void OnPlayerRespawned(int value)
		{
			Player.Instance.gameObject.layer = LayerMask.NameToLayer("IgnoreAsteroidCollision");
			_animator.SetTrigger("Explosion");
		}
		
		// Проверка под конец анимации взрыва
		private void OnPlayerDied()
		{
			GameInput.Instance.SpaceshipInputActions.Keyboard.Enable();
			GameInput.Instance.SpaceshipInputActions.UI.Disable();
			Player.Instance.gameObject.layer =  LayerMask.NameToLayer("Player");
		}

		private IEnumerator BlinkingRoutine()
		{
			Player.Instance.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
			GameInput.Instance.SpaceshipInputActions.Keyboard.Enable();
			GameInput.Instance.SpaceshipInputActions.UI.Disable();
			
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
			Player.Instance.gameObject.layer =  LayerMask.NameToLayer("Player");
		}
	}
}
