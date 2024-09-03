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
					// Спавним астеройды внутри нормализованого круга, что бы они появлялись на краю
					// На некотором растояниии от точки спавна.
					Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistanse;
					Vector3 spawnPoint = transform.position + spawnDirection;
					
					// Для каждого астеройда выбираем случайный угол на готорый он откланится при спауне
					float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);
					// Создаем вращение с заданым углом и осью вокруг которой будет происходить вращение
					Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
					
					Asteroid asteroid = Instantiate(_asteroidPrefab, spawnPoint, rotation);
					asteroid.Size = Random.Range(asteroid.MinSize, asteroid.MaxSize);
					
					// Направление отрицательное так как изначально астероид движется из крруга, а надо в круг
					asteroid.SetTrajectory(rotation * -spawnDirection);
				}
				
				yield return new WaitForSeconds(_spawnDelay);
			}
		}
	}
}