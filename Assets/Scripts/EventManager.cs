using System;
using UnityEngine;

namespace Asteroids
{
	public class EventManager : MonoBehaviour
	{
		public static event Action  PlayerShooted;
		
		public static void OnPlayerShooted() => PlayerShooted?.Invoke();

	}
}