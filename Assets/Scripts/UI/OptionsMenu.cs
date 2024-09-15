using UnityEngine;

namespace Asteroids
{
	public class OptionsMenu : Menu
	{
		new void OnEnable()
		{
			Time.timeScale = 0;
			SoundManager.Instance.PauseMusic();
			GameInput.Instance.SpaceshipInputActions.Disable();
		}

		void OnDisable()
		{
			gameObject.SetActive(false);
			GameInput.Instance.SpaceshipInputActions.Enable();
		}
	}
}