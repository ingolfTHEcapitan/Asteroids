using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(Animator))]
	public class PlayerVisual : MonoBehaviour
	{
		public static PlayerVisual Instance { get; private set;}
		[SerializeField] private float blinkInterval = 0.25f; // Интервал мигания в секундах
		[SerializeField] private float blinkDuration = 3f;   // Продолжительность мигания в секундах

		private Animator _animator;
		private SpriteRenderer _spriteRenderer;
		
		
		void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
				
			_animator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void Update()
		{
			_animator.SetFloat("Speed", GameInput.Instance.MovementInput.magnitude);
		}
		
		public IEnumerator BkinlingRoutine()
		{
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
