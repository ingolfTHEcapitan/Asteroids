using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
	public class PlayerRespawner : MonoBehaviour
	{
		[SerializeField] private float _respawnTime = 1.0f;
		
		private Player _player;
		
		private void OnEnable() => GameEvents.PlayerDied += OnPlayerDied;

		private void OnPlayerDied()
		{
			_player = Player.Instance;
			
			_player.CurrentLives -= 1;
			
			if(Player.Instance.CurrentLives <= 0)
				GameOver();
			else
				StartCoroutine(RespawnRoutine());

		}
		
		private IEnumerator RespawnRoutine()
		{
			yield return new WaitForSeconds(_respawnTime);
			
			_player.gameObject.SetActive(true);
			_player.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            _player.gameObject.layer = LayerMask.NameToLayer("IgnoreAsteroidCollision");
			StartCoroutine(PlayerVisual.Instance.BkinlingRoutine());
		}
		
		public void GameOver()
		{
			SceneManager.LoadScene(0);
		}
	}
}