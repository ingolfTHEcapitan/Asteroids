using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Asteroids
{
	public class GameEvents : MonoBehaviour
	{
		public static event Action  PlayerShooted;
		public static event Action PlayerDied;
		public static event Action<Collider2D> ExitTriggerFired;
		
		public static void OnPlayerShooted() => PlayerShooted?.Invoke();
		public static void OnExitTriggerFired(Collider2D collider) => ExitTriggerFired?.Invoke(collider);
		public static void OnPlayerDied() => PlayerDied?.Invoke();
		
	}
}