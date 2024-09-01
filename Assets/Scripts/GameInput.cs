using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
	public static GameInput Instance { get; private set;}
	private SpaceshipInputActions _spaceshipInputActions;
	
	public Vector2 MovementInput { get => _spaceshipInputActions.Keyboard.Move.ReadValue<Vector2>();}
	public float RotationInput { get => _spaceshipInputActions.Keyboard.Rotation.ReadValue<float>();}
	public SpaceshipInputActions SpaceshipInputActions { get => _spaceshipInputActions;}

	void Awake()
	{
		Instance = this;
		_spaceshipInputActions = new SpaceshipInputActions();
		_spaceshipInputActions.Enable();	
	}
	

}
