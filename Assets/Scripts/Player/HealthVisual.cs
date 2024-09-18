using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
	public class HealthVisual : MonoBehaviour
	{
		[SerializeField] private Image[] _hearts;
		[SerializeField] private Sprite _emptyHeart;
		[SerializeField] private Sprite _fullHeart;
		
		private int _value;
		private int _maxValue;
		
		
		private void Awake() 
		{
			_maxValue = Player.Instance.MaxHealth;
			
			GameEvents.PlayerRespawned += HealthChange;
			GameEvents.PlayerDied += OnPlayerDied;
		}
		
		private void HealthChange(int currentHealth)
		{
			for (int i = 0; i < _hearts.Length; i++)
			{
				if (i < currentHealth)
				{
					_hearts[i].sprite = _fullHeart;
				}
				else
				{
					_hearts[i].sprite = _emptyHeart;
				}
			}
		}
		
		private void OnPlayerDied()
		{
			foreach (var heart in _hearts)
			{
				heart.sprite = _fullHeart;
			}
		}
		
	}
}