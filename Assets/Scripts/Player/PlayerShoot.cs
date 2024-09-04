using UnityEngine;

namespace Asteroids
{
	public class PlayerShoot : MonoBehaviour
	{
		[SerializeField] private Laser _laserPrafab;
		[SerializeField] private Transform _laserSpawnPoint;
		[SerializeField] private float _fireRate;
		
		private float _curentReloadTime;

		private void OnEnable() => GameEvents.PlayerShooted += OnPlayerShooted;
		private void OnDisable() => GameEvents.PlayerShooted -= OnPlayerShooted;
		private void OnDestroy() => GameEvents.PlayerShooted -= OnPlayerShooted;
		
		public void OnPlayerShooted()
		{
			if (_curentReloadTime <= 0)
			{
				Laser laser = Instantiate(_laserPrafab, _laserSpawnPoint.position, transform.rotation);
				laser.Project(transform.up);
				_curentReloadTime = 1 / _fireRate;
			}
		}

		public void Update()
		{
			if (_curentReloadTime > 0)
			{
				_curentReloadTime -= Time.deltaTime;
			}
		}
	}
}