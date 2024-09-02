using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerMoving : MonoBehaviour
	{
		[SerializeField] private float _trustSpeed;
		[SerializeField] private float _turnSpeed;
		[SerializeField] private ScreenBounds _screenBounds;
		
		private Rigidbody2D _rigidbody;
		private Vector2 _inputMoving;
		private float _inputRotation;
		
		void Awake()
		{
			_rigidbody = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			_inputMoving = GameInput.Instance.MovementInput;
			_inputRotation = GameInput.Instance.RotationInput;	
		}
		
		void FixedUpdate()
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
	}
}
