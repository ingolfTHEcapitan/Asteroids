using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(Animator))]
	public class PlayerVisual : MonoBehaviour
	{
		private Animator _animator;
		
		void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		
		void Update()
		{
			_animator.SetFloat("Speed", GameInput.Instance.MovementInput.magnitude);
		}
	}
}
