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
		
		void OnEnable() => GameEvents.PlayerDied += OnPlayerDied;
		void OnDestroy() => GameEvents.PlayerDied -= OnPlayerDied;
		
		private void OnPlayerDied()
		{
			CurrentLives -= 1;
			
			if(CurrentLives <= 0)
				GameOver();
		}
		
		public void GameOver()
		{
			SceneManager.LoadScene(0);
		}
	}
}