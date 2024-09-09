using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
	public class Player : MonoBehaviour, IScreenWrappable
	{
		[SerializeField] private int _maxLives = 3;
		
		
		public static Player Instance { get; private set; }
		public int CurrentLives {get; set;}
		
		void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			CurrentLives = _maxLives;
		}
		
		public void Update()
		{
			if(CurrentLives <= 0)
				NewGame();
		}

		private void NewGame()
		{
			GameEvents.OnNewGameStarted();

			CurrentLives = _maxLives;
			transform.position = Vector3.zero;
		}
	}
}