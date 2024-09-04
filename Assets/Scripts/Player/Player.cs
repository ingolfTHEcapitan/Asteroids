using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
	public class Player : MonoBehaviour, IScreenWrappable
	{
		[SerializeField] private int _maxLives = 3;
		[SerializeField] private float blinkInterval = 0.25f; // Интервал мигания в секундах
		[SerializeField] private float blinkDuration = 3f;   // Продолжительность мигания в секундах

		[SerializeField] private SpriteRenderer _spriteRenderer;
		private int _currentLives;

		void Awake()
		{
			_currentLives = _maxLives;
		}

		private void OnEnable() => GameEvents.PlayerDied += OnPlayerDied;
		private void OnDestroy() => GameEvents.PlayerDied -= OnPlayerDied;

		private void OnPlayerDied()
		{
			_currentLives--;
			StartCoroutine(DiedRotine());
		}

		private IEnumerator DiedRotine()
		{
			gameObject.layer = LayerMask.NameToLayer("IgnoreAsteroidCollision");
			_spriteRenderer.enabled = false;
			transform.position = Vector3.zero;
			transform.right = Vector3.zero;
			yield return new WaitForSeconds(2);


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
			gameObject.layer = LayerMask.NameToLayer("Player");
		}
	}
}