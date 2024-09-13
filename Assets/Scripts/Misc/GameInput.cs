using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids
{
	public class GameInput : MonoBehaviour
	{
		public static GameInput Instance { get; private set;}

		public Vector2 MovementInput { get => SpaceshipInputActions.Keyboard.Move.ReadValue<Vector2>();}
		public float RotationInput { get => SpaceshipInputActions.Keyboard.Rotation.ReadValue<float>();}
		public SpaceshipInputActions SpaceshipInputActions { get; set;}

		void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			SpaceshipInputActions = new SpaceshipInputActions();
			SpaceshipInputActions.UI.Enable();
			SpaceshipInputActions.Keyboard.Disable();
			
		}
		
		void OnEnable() => GameEvents.PlayerTakeHit += OnPlayerTakeHit;
		
		private void OnPlayerTakeHit()
		{
			SpaceshipInputActions.UI.Enable();
			SpaceshipInputActions.Keyboard.Disable();
		}
	}
}
