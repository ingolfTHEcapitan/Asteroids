using UnityEngine;

namespace Asteroids
{
	public class OptionsMenu : Menu
	{
		new void OnEnable()
		{
			Time.timeScale = 0;
			SoundManager.Instance.AudioMute(true);
			GameInput.Instance.SpaceshipInputActions.Disable();
		}

		void OnDisable()
		{
			gameObject.SetActive(false);
			GameInput.Instance.SpaceshipInputActions.UI.Enable();
		}
	}
}