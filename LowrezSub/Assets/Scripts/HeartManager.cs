using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour {

	public void PlaySound(GameObject sound)
	{
		Instantiate (sound, transform);
	}
}
