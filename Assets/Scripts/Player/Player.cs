using Asteroids.ScreenWrapping;
using UnityEngine;

namespace Asteroids
{
	public class Player : MonoBehaviour, IScreenWrappable
	{
		[SerializeField] private int _maxHealth = 3;
		
		public static Player Instance { get; private set; }
		public int CurrentHealth {get; private set;}
		
		
        public void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			CurrentHealth = _maxHealth;
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
			CurrentHealth = _maxHealth;
			transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		}
	}
}