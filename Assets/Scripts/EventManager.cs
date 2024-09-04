using System;
using UnityEngine;

namespace Asteroids
{
	public class EventManager : MonoBehaviour
	{
		public static event Action  PlayerShooted;
		public static event Action<Collider2D> ExitTriggerFired;
		
		public static void OnPlayerShooted() => PlayerShooted?.Invoke();
		public static void OnExitTriggerFired(Collider2D collider) => ExitTriggerFired?.Invoke(collider);

	}
}