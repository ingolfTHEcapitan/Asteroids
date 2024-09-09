using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids
{
	public class GameInput : MonoBehaviour
	{
		public static GameInput Instance { get; private set;}

		public static PlayerInput PlayerInput { get; set;}
		private InputAction _moveInputAction;
		private InputAction _rotationInputAction;
		private InputAction _shootInputAction;
		
		public Vector2 MovementInput { get => _moveInputAction.ReadValue<Vector2>();}
		public float RotationInput { get => _rotationInputAction.ReadValue<float>();}
		public bool ShootInput { get => _shootInputAction.WasPressedThisFrame();}

		void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			PlayerInput = GetComponent<PlayerInput>();
			
			
			_moveInputAction = PlayerInput.actions["Move"];
			_rotationInputAction = PlayerInput.actions["Rotation"];
			_shootInputAction = PlayerInput.actions["Shoot"];
			
			_shootInputAction.performed += (_) => GameEvents.OnPlayerShooted();
		}
		
		void OnEnable() => GameEvents.PlayerDied += OnPlayerDied;
		void OnDestroy() => GameEvents.PlayerDied -= OnPlayerDied;
		
		private void OnPlayerDied()
		{
			PlayerInput.SwitchCurrentActionMap("UI");
		}
	}
}
