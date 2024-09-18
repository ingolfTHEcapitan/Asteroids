using UnityEngine;

namespace Asteroids
{
	public abstract class Menu : MonoBehaviour
	{
		public void OnEnable()
		{
			Time.timeScale = 0;
		}
		
		private void Start()
		{
			SoundManager.Instance.AudioMute(true);
		}

		public virtual void Pause()
		{
			Time.timeScale = 0;
			SoundManager.Instance.AudioMute(true);
			GameInput.Instance.SpaceshipInputActions.UI.Enable();
			GameInput.Instance.SpaceshipInputActions.Keyboard.Disable();
			GameInput.Instance.SpaceshipInputActions.Keyboard.Shoot.performed -= (_) => GameEvents.OnPlayerShooted();
			GameInput.Instance.SpaceshipInputActions.UI.Pause.performed += (_) => UnPause();
		}
		
		public virtual void UnPause()
		{
			Time.timeScale = 1;
			SoundManager.Instance.AudioMute(false);
			GameInput.Instance.SpaceshipInputActions.Keyboard.Enable();
			GameInput.Instance.SpaceshipInputActions.UI.Disable();
			GameInput.Instance.SpaceshipInputActions.Keyboard.Shoot.performed += (_) => GameEvents.OnPlayerShooted();
			GameInput.Instance.SpaceshipInputActions.Keyboard.Pause.performed += (_) => Pause();
		}
	}
}