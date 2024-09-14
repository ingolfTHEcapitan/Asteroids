using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class PauseMenu : Menu
	{
		void OnEnable()
		{
			GameInput.Instance.SpaceshipInputActions.UI.Pause.performed += (_) => gameObject.SetActive(false);

		}
		
		
		public override void UnPause()
		{
			base.UnPause();
			
			
		}
	}
}