using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
	public class Player : MonoBehaviour, IScreenWrappable
	{
		public static Player Instance { get; private set; }
		
		[SerializeField] private int _maxLives = 3;
		
		public int CurrentLives {get; set;}

		void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(Instance);
			else
				Instance = this;
			
			CurrentLives = _maxLives;
		}
	}
}