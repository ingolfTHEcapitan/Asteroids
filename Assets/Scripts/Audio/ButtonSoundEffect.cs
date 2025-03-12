using UnityEngine;
using UnityEngine.EventSystems;

namespace Asteroids
{
	public class ButtonSoundEffect : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
	{
		[SerializeField] private AudioClip _pointerDown;
		[SerializeField] private AudioClip _pointerEnter;
		[SerializeField] private AudioSource _audioSource;

		public void OnPointerDown(PointerEventData eventData)
		{
			_audioSource.PlayOneShot(_pointerDown);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_audioSource.PlayOneShot(_pointerEnter);
		}
	}
}