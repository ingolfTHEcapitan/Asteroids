using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private GameObject _optionsMenu;
		
		private void Awake()
		{
			Time.timeScale = 0;
		}

		private void Start()
		{
			SoundManager.Instance.PauseMusic();
		}
		
		public void Play()
		{
			Time.timeScale = 1;
			SoundManager.Instance.UnPauseMusic();
			GameInput.Instance.SpaceshipInputActions.Enable();
			gameObject.SetActive(false);
			GameInput.Instance.SpaceshipInputActions.Keyboard.Shoot.performed += (_) => GameEvents.OnPlayerShooted();
		}
		
		public void Options()
		{
			gameObject.SetActive(false);
			_optionsMenu.SetActive(true);
		}
		
		public void Exit()
		{
			Application.Quit();
		}
		

	}
}