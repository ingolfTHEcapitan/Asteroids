using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Asteroids
{
	public class AudioMixerControler : MonoBehaviour
	{
		[Header("AudioMixer")]
		[SerializeField] private AudioMixer _audioMixer;
		
		[Header("Sliders")]
		[SerializeField] private Slider _masterSlider;
		[SerializeField] private Slider _effectsSlider;
		[SerializeField] private Slider _musicSlider;
		[SerializeField] private Slider _uiSlider;
		
		private Dictionary<string, Slider> _volumeSliders;
		
		private void Start() 
		{
			InitializeSliders();
			LoadVolumeSettings();
		}

		private void InitializeSliders()
		{
			_volumeSliders = new Dictionary<string, Slider>
			{
				{"MasterVolume", _masterSlider},
				{"EffectsVolume", _effectsSlider},
				{"MusicVolume", _musicSlider},
				{"UIVolume", _uiSlider}
			};
		}
		
		private void SetVolume(string volumeName, float volume)
		{
			_audioMixer.SetFloat(volumeName, Mathf.Log10(volume) * 20);
			PlayerPrefs.SetFloat(volumeName, volume);
		}
		
		private void LoadVolumeSettings()
		{
			foreach (var kvpSlider in _volumeSliders)
			{
				string volumeName = kvpSlider.Key;
				Slider slider = kvpSlider.Value;
				
				float savedVolume = PlayerPrefs.GetFloat(volumeName, 1.0f);
				slider.value = savedVolume;
				SetVolume(volumeName, savedVolume);
			}
		}

		public void OnMasterVolumeChange(float volume) => SetVolume("MasterVolume", volume);
		public void OnEffectsVolumeChange(float volume) => SetVolume("EffectsVolume", volume);
		public void OnMusicVolumeChange(float volume) => SetVolume("MusicVolume", volume);
		public void OnUIVolumeChange(float volume) => SetVolume("UIVolume", volume);
	}
}