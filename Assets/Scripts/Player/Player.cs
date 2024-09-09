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
		public int CurrentLives {get; private set;}
		
		void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			CurrentLives = _maxLives;
		}

		private void OnEnable()
		{
			GameEvents.PlayerTakeHit += OnPlayerTakeHit;
			GameEvents.PlayerDied += OnPlayerDied;
		}

		private void OnPlayerTakeHit()
		{
			CurrentLives--;
			GameEvents.OnPlayerRespawned();
		}
		
		private void OnPlayerDied()
		{
			CurrentLives = _maxLives;
			transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		}
	}
}