using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Asteroids
{
	public abstract class Menu : MonoBehaviour
	{
		private void OnEnable()
		{
			Time.timeScale = 0;
		}
		
		private void Start()
		{
			SoundManager.Instance.PauseMusic();
		}

		public virtual void Pause()
		{
			Time.timeScale = 0;
			SoundManager.Instance.PauseMusic();
			GameInput.Instance.SpaceshipInputActions.Keyboard.Disable();
			GameInput.Instance.SpaceshipInputActions.UI.Enable();
		}
		
		public virtual void UnPause()
		{
			Time.timeScale = 1;
			SoundManager.Instance.UnPauseMusic();
			GameInput.Instance.SpaceshipInputActions.Keyboard.Enable();
			GameInput.Instance.SpaceshipInputActions.UI.Disable();
		}
	}
}