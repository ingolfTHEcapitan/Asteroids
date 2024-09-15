using System;
using UnityEngine;

namespace Asteroids
{
	public class GameEvents : MonoBehaviour
	{
		public static event Action  PlayerShooted;
		public static event Action PlayerRespawned;
		public static event Action PlayerTakeHit;
		public static event Action PlayerDied;
		public static event Action<Collider2D> ExitTriggerFired;
		public static event Action<Asteroid> AsteroidExploded;
		public static event Action GamePaused;
		public static event Action GameUnPaused;
		
		public static void OnPlayerShooted() => PlayerShooted?.Invoke();
		public static void OnPlayerRespawned() => PlayerRespawned?.Invoke();
		public static void OnPlayerTakeHit() => PlayerTakeHit?.Invoke();
		public static void OnPlayerDied() => PlayerDied?.Invoke();
		public static void OnExitTriggerFired(Collider2D collider) => ExitTriggerFired?.Invoke(collider);
		public static void OnAsteroidExploded(Asteroid asteroid) => AsteroidExploded?.Invoke(asteroid);
		public static void OnGamePaused() => GamePaused?.Invoke();
		public static void OnGameUnPaused() => GameUnPaused?.Invoke();
	}
}