using UnityEngine;

namespace Asteroids
{
	public class GameInput : MonoBehaviour
	{
		public static GameInput Instance { get; private set;}

		public Vector2 MovementInput => SpaceshipInputActions.Keyboard.Move.ReadValue<Vector2>();
		public float RotationInput => SpaceshipInputActions.Keyboard.Rotation.ReadValue<float>();
		public SpaceshipInputActions SpaceshipInputActions { get; private set;}

		private void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			SpaceshipInputActions = new SpaceshipInputActions();
			SpaceshipInputActions.UI.Enable();
			SpaceshipInputActions.Keyboard.Disable();
			
		}

		private void OnEnable() => GameEvents.PlayerTakeHit += OnPlayerTakeHit;
		
		private void OnPlayerTakeHit()
		{
			SpaceshipInputActions.UI.Enable();
			SpaceshipInputActions.Keyboard.Disable();
		}
	}
}
