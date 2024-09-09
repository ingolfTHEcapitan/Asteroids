using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(Animator))]
	public class PlayerVisual : MonoBehaviour
	{
		[SerializeField] private float blinkInterval = 0.25f; // �������� ������� � ��������
		[SerializeField] private float blinkDuration = 3f;   // ����������������� ������� � ��������

		private Animator _animator;
		private SpriteRenderer _spriteRenderer;
		
		void Awake()
		{
			_animator = GetComponent<Animator>();
			_spriteRenderer = GetComponent<SpriteRenderer>();	
		}
		
		void OnEnable() => GameEvents.PlayerDied += OnPlayerDied;
		void OnDestroy() => GameEvents.PlayerDied -= OnPlayerDied;

		void Update()
		{
			_animator.SetFloat("Speed", GameInput.Instance.MovementInput.magnitude);
		}
		
		private void OnPlayerDied()
		{
			if (Player.Instance.CurrentLives <=0)
			{
				GameInput.PlayerInput.SwitchCurrentActionMap("Keyboard");
				return;
			}
				
			Player.Instance.gameObject.layer = LayerMask.NameToLayer("IgnoreAsteroidCollision");
			_animator.SetTrigger("Die");
		}
		
		public void StartBkinling()
		{
			StartCoroutine(BkinlingRoutine());
		}
		
		
		public IEnumerator BkinlingRoutine()
		{
			_spriteRenderer.enabled = false;
			yield return new WaitForSeconds(1);
			_spriteRenderer.enabled = true;
			GameInput.PlayerInput.SwitchCurrentActionMap("Keyboard");
			
			float endTime = Time.time + blinkDuration;
			while (Time.time < endTime)
			{
				// ����������� ��������� �������
				_spriteRenderer.enabled = !_spriteRenderer.enabled;

				// ���� ��������� ��������
				yield return new WaitForSeconds(blinkInterval);
			}

			// ��������, ��� ������ ����� �� ��������� �������
			_spriteRenderer.enabled = true;
			Player.Instance.gameObject.layer = LayerMask.NameToLayer("Player");
		}
	}
}
