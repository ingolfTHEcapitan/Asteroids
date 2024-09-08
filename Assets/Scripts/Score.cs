using TMPro;
using UnityEngine;

namespace Asteroids
{
	public class Score : MonoBehaviour
{
	public TextMeshProUGUI ScoreText;
	public TextMeshProUGUI HighScoreText;
	private int _scoreCount;

	private void Awake()
	{
		GameEvents.AsteroidExplosion += IncreaseScore;
	}
	
	private void Start()
	{
		// ������������� ����� ��� ���� High Score �� ����������� ������.
		HighScoreText.SetText($"High Score: {PlayerPrefs.GetInt("highScore", 0)}");
	}
	
	private void IncreaseScore(Asteroid asteroid)
	{
		// ��������� ���-�� ����� � ����������� �� ������� ���������
		_scoreCount += (int)((asteroid.MaxSize - asteroid.Size) * 100);
		
		// ��������� ����� ��� ���� �������� �����.
		ScoreText.text = $"Score: {_scoreCount}";
		// ���������, ���������� �� ����� ������, � ��������� ��� ��� �������������.
		UpdateScore();
	}

	private void UpdateScore()
	{
		// ���������, ��������� �� ������� ���� ������ ���������.
		if (_scoreCount > PlayerPrefs.GetInt("highScore", 0))
		{
			// ���� ��, ��������� ������ ��������� � ����������� ������.
			PlayerPrefs.SetInt("highScore", _scoreCount);

			// ��������� ����� ��� ���� "������ ���������" �� ������.
			HighScoreText.SetText($"High Score: {PlayerPrefs.GetInt("highScore", _scoreCount)}");

			SetTextColor(Color.yellow);

		}
	}

	private void SetTextColor(Color color)
	{
		ScoreText.color = color;
		HighScoreText.color = color;
	}
	
	// public void ResetScore()
	// {
	// 	_scoreCount = 0;
	// 	ScoreText.SetText($"{_scoreCount}");
	// 	SetTextColor(Color.white);
	// }

	// public void ResetHighScore()
	// {
	// 	PlayerPrefs.DeleteKey("highScore");
	// 	HighScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
	// }
	
	
}
}
