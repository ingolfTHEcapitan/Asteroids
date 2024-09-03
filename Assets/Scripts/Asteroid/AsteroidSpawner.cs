using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class AsteroidSpawner : MonoBehaviour
	{
		[SerializeField] private Asteroid _asteroidPrefab;
		[SerializeField] private float _spawnDelay = 2.0f;
		[SerializeField] private float _spawnAmount = 3.0f;
		[SerializeField] private float _spawnDistanse = 15.0f;
		[SerializeField] private float _trajectoryVariance = 15.0f;
		
		
		
		private void Start()
		{
			StartCoroutine(SpawnRoutine());
		}

		private IEnumerator SpawnRoutine()
		{
			while (true)
			{
				for (int i = 0; i < _spawnAmount; i++)
				{
					// ������� ��������� ������ ��������������� �����, ��� �� ��� ���������� �� ����
					// �� ��������� ���������� �� ����� ������.
					Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistanse;
					Vector3 spawnPoint = transform.position + spawnDirection;
					
					// ��� ������� ��������� �������� ��������� ���� �� ������� �� ���������� ��� ������
					float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);
					// ������� �������� � ������� ����� � ���� ������ ������� ����� ����������� ��������
					Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
					
					Asteroid asteroid = Instantiate(_asteroidPrefab, spawnPoint, rotation);
					asteroid.Size = Random.Range(asteroid.MinSize, asteroid.MaxSize);
					
					// ����������� ������������� ��� ��� ���������� �������� �������� �� ������, � ���� � ����
					asteroid.SetTrajectory(rotation * -spawnDirection);
				}
				
				yield return new WaitForSeconds(_spawnDelay);
			}
		}
	}
}