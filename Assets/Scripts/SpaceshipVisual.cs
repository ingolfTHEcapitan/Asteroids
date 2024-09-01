using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipVisual : MonoBehaviour
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
