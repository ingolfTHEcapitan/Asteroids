using UnityEngine;

namespace Asteroids
{
	public class GameInput : MonoBehaviour
	{
		public static GameInput Instance { get; private set;}
		private SpaceshipInputActions _spaceshipInputActions;
		
		public Vector2 MovementInput { get => _spaceshipInputActions.Keyboard.Move.ReadValue<Vector2>();}
		public float RotationInput { get => _spaceshipInputActions.Keyboard.Rotation.ReadValue<float>();}
		public SpaceshipInputActions SpaceshipInputActions { get => _spaceshipInputActions;}

		void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			_spaceshipInputActions = new SpaceshipInputActions();
			_spaceshipInputActions.Enable();	
			
			_spaceshipInputActions.Keyboard.Shoot.performed += (_) => GameEvents.OnPlayerShooted();
		}
	}
}
