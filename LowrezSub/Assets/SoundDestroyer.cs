using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundDestroyer : MonoBehaviour {
	
	void Start () 
	{
		AudioSource audio = GetComponent<AudioSource> ();
		Destroy (gameObject, audio.clip.length);
	}

}
