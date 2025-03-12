using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	public class AsteroidSpawner : MonoBehaviour
	{
		[SerializeField] private Asteroid _asteroidPrefab;
		[SerializeField] private int _spawnAmount = 3;
		[SerializeField] private float _spawnDelay = 2.0f;
		[SerializeField] private float _spawnDistance = 15.0f;
		[SerializeField] private float _trajectoryVariance = 15.0f;
		[SerializeField] private int _maxAsteroidsAtTime = 10;
		
		private void Start()
		{
			GameEvents.OnPlayerDied();
			StartCoroutine(SpawnRoutine());
		}

		private void OnEnable() => GameEvents.AsteroidSplitted += SplitAsteroid;
		private void OnDisable() => GameEvents.AsteroidSplitted -= SplitAsteroid;

		private IEnumerator SpawnRoutine()
		{
			while (true)
			{
				for (int i = 0; i < _spawnAmount; i++)
				{
					if (Asteroid.Asteroids.Count <= _maxAsteroidsAtTime)
					{
						// Спавним астеройды внутри нормализованного круга, что бы они появлялись на краю
						// На некотором растоянии от точки спавна.
						Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistance;
						Vector3 spawnPoint = transform.position + spawnDirection;
					
						// Для каждого астеройда выбираем случайный угол на который он отклонится при спавне
						float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);
						// Создаем вращение с заданным углом и осью вокруг которой будет происходить вращение
						Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
					
						Asteroid asteroid = Instantiate(_asteroidPrefab, spawnPoint, rotation);
						// Направление отрицательное так как изначально астероид движется из круга, а надо в круг
						asteroid.ParentInitialize(rotation * -spawnDirection);
					}
					else
					{
						yield return new WaitUntil(() => Asteroid.Asteroids.Count >= _maxAsteroidsAtTime);
					}
				}
				
				yield return new WaitForSeconds(_spawnDelay);
			}
		}
		
		private void SplitAsteroid(Asteroid asteroid)
		{
			for (int i = 0; i < 2; i++)
			{
				if(Asteroid.Asteroids.Count>= _maxAsteroidsAtTime)
					return;
				
				Vector2 position = asteroid.transform.position;
				position += Random.insideUnitCircle * 0.5f;
			
				Asteroid halfAsteroid = Instantiate(asteroid, position, asteroid.transform.rotation);
				halfAsteroid.ChildInitialize(Random.insideUnitCircle.normalized, asteroid);
			}
		}	
	}
}