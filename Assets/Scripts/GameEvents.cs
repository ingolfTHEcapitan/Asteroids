using System;
using UnityEngine;

namespace Asteroids
{
	public class GameEvents : MonoBehaviour
	{
		public static event Action  PlayerShooted;
		public static event Action PlayerDied;
		public static event Action NewGameStarted;
		public static event Action<Collider2D> ExitTriggerFired;
		public static event Action<Asteroid> AsteroidExploded;
		
		public static void OnPlayerShooted() => PlayerShooted?.Invoke();
		public static void OnPlayerDied() => PlayerDied?.Invoke();
		public static void OnNewGameStarted() => NewGameStarted?.Invoke();
		public static void OnExitTriggerFired(Collider2D collider) => ExitTriggerFired?.Invoke(collider);
		public static void OnAsteroidExplosion(Asteroid asteroid) => AsteroidExploded?.Invoke(asteroid);
	}
}