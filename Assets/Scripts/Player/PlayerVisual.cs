using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(Animator))]
	public class PlayerVisual : MonoBehaviour
	{
		public static PlayerVisual Instance { get; private set;}
		[SerializeField] private float blinkInterval = 0.25f; // �������� ������� � ��������
		[SerializeField] private float blinkDuration = 3f;   // ����������������� ������� � ��������

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
