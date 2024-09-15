using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class PauseMenu : Menu
	{
		new void OnEnable()
		{
			base.OnEnable();
			GameInput.Instance.SpaceshipInputActions.Keyboard.Pause.performed -= (_) => gameObject.SetActive(true);
			GameInput.Instance.SpaceshipInputActions.UI.Pause.performed += (_) => gameObject.SetActive(true);
		}


		public override void UnPause()
		{
			base.UnPause();
			gameObject.SetActive(false);
		}

		void OnDisable()
		{
			GameInput.Instance.SpaceshipInputActions.Keyboard.Pause.performed += (_) => gameObject.SetActive(true);
			GameInput.Instance.SpaceshipInputActions.UI.Pause.performed -= (_) => gameObject.SetActive(true);
		}

	}
}