using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerMoving : MonoBehaviour
	{
		[SerializeField] private float _trustSpeed;
		[SerializeField] private float _turnSpeed;

		private Rigidbody2D _rigidbody;
		private Vector2 _inputMoving;
		private float _inputRotation;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		public void Update()
		{
			_inputMoving = GameInput.Instance.MovementInput;
			_inputRotation = GameInput.Instance.RotationInput;	
		}

		private void FixedUpdate()
		{
			if (_inputMoving.magnitude != 0.0f)
			{
				_rigidbody.AddForce(transform.up * _trustSpeed);
			}
			
			if (_inputRotation != 0.0f)
			{
				_rigidbody.AddTorque(_inputRotation * _turnSpeed);
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.GetComponent<Asteroid>() != null)
			{
				_rigidbody.velocity = Vector2.zero;
				_rigidbody.angularVelocity = 0.0f;
				
				GameEvents.OnPlayerTakeHit();
			}
		}                       
	}
}
