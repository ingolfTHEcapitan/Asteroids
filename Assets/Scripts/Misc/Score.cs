using TMPro;
using UnityEngine;

namespace Asteroids
{
	public class Score : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private TextMeshProUGUI _highScoreText;
		
		private int _scoreCount;
		
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
			_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
		}
		
		public void ResetHighScore()
		{
			PlayerPrefs.DeleteKey("highScore");
			_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
		}
		
		private void IncreaseScore(Asteroid asteroid)
		{
			// Вычисляем кол-во очков в зависимости от размера астеройда
			_scoreCount += (int)((asteroid.MaxSize - asteroid.Size) * 100);
			
			_scoreText.text = $"{_scoreCount}";
			UpdateScore();
		}

		private void UpdateScore()
		{
			if (_scoreCount > PlayerPrefs.GetInt("highScore", 0))
			{
				PlayerPrefs.SetInt("highScore", _scoreCount);
				
				_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", _scoreCount)}");
			}
		}
		
		private void ResetScore()
		{
			_scoreCount = 0;
			_scoreText.SetText($"{_scoreCount}");
		}
	}
}
