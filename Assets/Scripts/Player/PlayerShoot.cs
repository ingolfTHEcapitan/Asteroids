using UnityEngine;

namespace Asteroids
{
	public class PlayerShoot : MonoBehaviour
	{
		[SerializeField] private Laser _laserPrafab;
		[SerializeField] private Transform _laserSpawnPoint;
		[SerializeField] private float _fireRate;
		
		private float _currentReloadTime;

		private void OnEnable() => GameEvents.PlayerShooted += OnPlayerShooted;
		private void OnDestroy() => GameEvents.PlayerShooted -= OnPlayerShooted;

		private void OnPlayerShooted()
		{
			if (_currentReloadTime <= 0)
			{
				Laser laser = Instantiate(_laserPrafab, _laserSpawnPoint.position, transform.rotation);
				laser.Project(transform.up);
				_currentReloadTime = 1 / _fireRate;
			}
		}

		public void Update()
		{
			if (_currentReloadTime > 0)
			{
				_currentReloadTime -= Time.deltaTime;
			}
		}
	}
}