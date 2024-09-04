using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class Player : MonoBehaviour, IScreenWrappable
	{
		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.gameObject.GetComponent<Asteroid>() != null)
			{
				
			}
		}
	}
}