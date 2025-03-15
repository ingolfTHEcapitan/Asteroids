using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using Asteroids.DataPersistence;
using Asteroids.DataPersistence.Data;

namespace Asteroids
{
	public class AudioMixerController : MonoBehaviour
	{
		[Header("AudioMixer")]
		[SerializeField] private AudioMixer _audioMixer;
		
		[Header("Sliders")]
		[SerializeField] private Slider _masterSlider;
		[SerializeField] private Slider _effectsSlider;
		[SerializeField] private Slider _musicSlider;
		[SerializeField] private Slider _uiSlider;
		
		private Dictionary<string, Slider> _volumeSliders;
		private SaveData _gameSave;
		
		private void Start() 
		{
			_gameSave = SaveSystem.Load();
			InitializeSliders();
		}

		private void InitializeSliders()
		{
			SetupSlider("MasterVolume", _masterSlider, _gameSave.MasterVolume);
			SetupSlider("EffectsVolume", _effectsSlider, _gameSave.EffectsVolume);
			SetupSlider("MusicVolume", _musicSlider, _gameSave.MusicVolume);
			SetupSlider("UIVolume", _uiSlider, _gameSave.UIVolume);
		}
		
		private void SetupSlider(string volumeName, Slider slider, float SavedValue)
		{
			slider.SetValueWithoutNotify(SavedValue);
			slider.onValueChanged.AddListener((volume)=>
			{
				SetVolume(volumeName, volume);
			});
			
		}
		
		private void SetVolume(string volumeName, float volume)
		{
			_audioMixer.SetFloat(volumeName, Mathf.Log10(volume) * 20);
			UpdateSaveData(volumeName, volume);
		}
		
		private void UpdateSaveData(string volumeName, float volume)
		{
			switch (volumeName)
			{
				case "MasterVolume": _gameSave.MasterVolume = volume; break;
				case "EffectsVolume": _gameSave.EffectsVolume = volume; break;
				case "MusicVolume": _gameSave.MusicVolume = volume; break;
				case "UIVolume": _gameSave.UIVolume = volume; break;
			}
			
			SaveSystem.Save(_gameSave);
		}
	}
}