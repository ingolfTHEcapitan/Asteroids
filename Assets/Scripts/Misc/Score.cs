using Asteroids.DataPersistence;
using Asteroids.DataPersistence.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Asteroids
{
	public class Score : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _currentScoreText;
		[SerializeField] private TextMeshProUGUI _highScoreText;
		
		private int _currentScore;
		private SaveData _gameSave;
		
		private void OnEnable()
		{
			GameEvents.AsteroidExploded += IncreaseScore;
			GameEvents.PlayerDied += ResetScore;
		}

		private void OnDisable()
		{
			GameEvents.AsteroidExploded -= IncreaseScore;
			GameEvents.PlayerDied -= ResetScore;
		}

		private void Start()
		{
			_gameSave = SaveSystem.Load();
			UpdateHighScoreDisplay();
		}
		
		public void ResetHighScore()
		{
			_gameSave.HighScore = 0;
			SaveSystem.Save(_gameSave);
			UpdateHighScoreDisplay();
		}
		
		private void IncreaseScore(Asteroid asteroid)
		{
			// Вычисляем кол-во очков в зависимости от размера астеройда
			_currentScore += (int)((asteroid.MaxSize - asteroid.Size) * 100);
			_currentScoreText.text = _currentScore.ToString();
			CheckHighScore();
		}

		private void CheckHighScore()
		{
			if (_currentScore > _gameSave.HighScore)
			{
				_gameSave.HighScore = _currentScore;
				SaveSystem.Save(_gameSave);
				UpdateHighScoreDisplay();
			}
		}
		
		private void ResetScore()
		{
			_currentScore = 0;
			_currentScoreText.text = _currentScore.ToString();
		}
		
		private void UpdateHighScoreDisplay()
		{
			_highScoreText.text = "Best:" + _gameSave.HighScore;
		}
	}
}
