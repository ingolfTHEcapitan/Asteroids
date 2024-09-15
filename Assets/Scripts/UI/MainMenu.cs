using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class MainMenu : Menu
	{
		[SerializeField] private GameObject _pauseMenu;

		public override void UnPause()
		{
			base.UnPause();
			GameInput.Instance.SpaceshipInputActions.Keyboard.Pause.performed += (_) => _pauseMenu.SetActive(true);
		}
		
		public void Exit()
		{
			Application.Quit();
		}
		
		void OnDisable()
		{
			GameInput.Instance.SpaceshipInputActions.Keyboard.Pause.performed -= (_) => _pauseMenu.SetActive(true);
		}
	}
}