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
		// ������������� ����� ��� ���� High Score �� ����������� ������.
		_highScoreText.SetText($"Record: {PlayerPrefs.GetInt("highScore", 0)}");
	}
	
	private void IncreaseScore(Asteroid asteroid)
	{
		// ��������� ���-�� ����� � ����������� �� ������� ���������
		_scoreCount += (int)((asteroid.MaxSize - asteroid.Size) * 100);
		
		// ��������� ����� ��� ���� �������� �����.
		_scoreText.text = $"Score: {_scoreCount}";
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
			_highScoreText.SetText($"Record: {PlayerPrefs.GetInt("highScore", _scoreCount)}");
		}
	}

	public void ResetScore()
	{
		_scoreCount = 0;
		_scoreText.SetText($"Score: {_scoreCount}");
	}

	// public void ResetHighScore()
	// {
	// 	PlayerPrefs.DeleteKey("highScore");
	// 	HighScoreText.SetText($"{PlayerPrefs.GetInt("highScore", 0)}");
	// }
	
	
}
}
