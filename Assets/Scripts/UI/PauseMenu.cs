
namespace Asteroids
{
	public class PauseMenu : Menu
	{
		new void OnEnable()
		{
			Pause();
		}

		public override void UnPause()
		{
			base.UnPause();
			gameObject.SetActive(false);
		}
	}
	
}