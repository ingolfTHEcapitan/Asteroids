using System;
using UnityEngine;

namespace Asteroids
{
	public enum SoundType
	{
		Laser,
		Explosion,
		Dead,
		Hurt
	}
	
	[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
	public class SoundManager : MonoBehaviour
	{
		[SerializeField] private Sound[] _soundList;
		
		private AudioSource _audioSource;
		
		public static SoundManager Instance { get; private set; }
		
		private void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
				
			_audioSource = GetComponent<AudioSource>();
		}
		
		private void OnEnable()
		{
			GameEvents.AsteroidExploded += (_) => PlaySound(SoundType.Explosion);
			GameEvents.PlayerShooted += () => PlaySound(SoundType.Laser, 0.4f);
			GameEvents.PlayerDied += () => PlaySound(SoundType.Dead);
			GameEvents.PlayerTakeHit += () => PlaySound(SoundType.Hurt);
			
			#if UNITY_EDITOR
			
			string[] soundNames = Enum.GetNames(typeof(SoundType));
			Array.Resize(ref _soundList, soundNames.Length);
			
			for (int i = 0; i < _soundList.Length; i++)
				_soundList[i].Name = soundNames[i];
				
			#endif
		}
		
		private void PlaySound(SoundType sound, float volume = 1.0f)
		{
			AudioClip[] clips = _soundList[(int)sound].AudioClips;
			AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
			
			_audioSource.PlayOneShot(randomClip, volume);
			_audioSource.volume = volume;
			_audioSource.pitch = UnityEngine.Random.Range(0.7f, 1.3f);
		}
	}
	
	[Serializable]
	public struct Sound
	{
		[SerializeField, HideInInspector] private string _name;
		[SerializeField] private AudioClip[] _audioClips;

		public string Name {set => _name = value; }
		public AudioClip[] AudioClips { get => _audioClips;}
	}
	
	
}