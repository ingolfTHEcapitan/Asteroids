using System;
using UnityEngine;

namespace Asteroids
{
	public class GameEvents : MonoBehaviour
	{
		public static event Action  PlayerShooted;
		public static event Action<int> PlayerRespawned;
		public static event Action PlayerTakeHit;
		public static event Action PlayerDied;
		public static event Action<Collider2D> ExitTriggerFired;
		public static event Action<Asteroid> AsteroidExploded;
		public static event Action<Asteroid> AsteroidSplitted;
		
		public static void OnPlayerShooted() => PlayerShooted?.Invoke();
		public static void OnPlayerRespawned(int value) => PlayerRespawned?.Invoke(value);
		public static void OnPlayerTakeHit() => PlayerTakeHit?.Invoke();
		public static void OnPlayerDied() => PlayerDied?.Invoke();
		public static void OnExitTriggerFired(Collider2D collider) => ExitTriggerFired?.Invoke(collider);
		public static void OnAsteroidExploded(Asteroid asteroid) => AsteroidExploded?.Invoke(asteroid);
		public static void OnAsteroidSplitted(Asteroid asteroid) => AsteroidSplitted?.Invoke(asteroid);

	}
}