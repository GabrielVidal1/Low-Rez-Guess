using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangageKeeper : MonoBehaviour {

	public static LangageKeeper langageKeeper;

	public Langage langage;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
		if (langageKeeper == null)
			langageKeeper = this;
		else if (langageKeeper != this)
			Destroy (gameObject);
	}
}	
