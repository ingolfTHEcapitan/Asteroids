using TMPro;
using UnityEngine;

namespace Asteroids
{
	public class Score : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private TextMeshProUGUI _highScoreText;
	private int _scoreCount;

	private void Awake()
	{
		GameEvents.AsteroidExploded += IncreaseScore;
	}
	
	void OnEnable() => GameEvents.PlayerDied += ResetScore;
	
	
	private void Start()
	{
		// Устанавливаем текст для поля High Score из сохраненных данных.
		_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
	}
	
	private void IncreaseScore(Asteroid asteroid)
	{
		// Вычисялем кол-во очков в зависимости от размера астеройда
		_scoreCount += (int)((asteroid.MaxSize - asteroid.Size) * 100);
		
		// Обновляем текст для поля текущего счета.
		_scoreText.text = $"{_scoreCount}";
		// Проверяем, установлен ли новый рекорд, и обновляем его при необходимости.
		UpdateScore();
	}

	private void UpdateScore()
	{
		// Проверяем, превышает ли текущий счет лучший результат.
		if (_scoreCount > PlayerPrefs.GetInt("highScore", 0))
		{
			// Если да, обновляем лучший результат в сохраненных данных.
			PlayerPrefs.SetInt("highScore", _scoreCount);

			// Обновляем текст для поля "Лучший результат" на экране.
			_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", _scoreCount)}");
		}
	}

	public void ResetScore()
	{
		_scoreCount = 0;
		_scoreText.SetText($"{_scoreCount}");
	}

	public void ResetHighScore()
	{
		PlayerPrefs.DeleteKey("highScore");
		_highScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
	}
	
	
}
}
