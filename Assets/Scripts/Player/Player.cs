using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
	public class Player : MonoBehaviour, IScreenWrappable
	{
		[SerializeField] private int _maxHealth = 3;
		
		public static Player Instance { get; private set; }
		public int CurrentHealth {get; private set;}
        public int MaxHealth { get => _maxHealth;}

        void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			CurrentHealth = MaxHealth;
		}

		private void OnEnable()
		{
			GameEvents.PlayerTakeHit += OnPlayerTakeHit;
			GameEvents.PlayerDied += OnPlayerDied;
		}

		private void OnPlayerTakeHit()
		{
			CurrentHealth--;
			GameEvents.OnPlayerRespawned(CurrentHealth);
		}
		
		private void OnPlayerDied()
		{
			CurrentHealth = MaxHealth;
			transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		}
	}
}