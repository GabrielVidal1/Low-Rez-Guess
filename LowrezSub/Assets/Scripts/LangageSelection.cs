using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangageSelection : MonoBehaviour {

	public Text demo;

	// dans l'ordre :
	/*
	 * anglais
	 * francais
	 * italien
	 * espagnol
	 * portugais
	 * allemand
	 */

	public Texture[] flags;

	/* dans l'ordre :
	 * farLeftFlag
	 * LeftFlag
	 * centerFlag
	 * RightFlag
	 * FarRightFlag
	 */

	public RawImage[] selectionPanel;

	public Animator mainMenuAnimator;


	int cursor = 0;

	Animator flagAnimator;

	bool canMove = true;

	AudioSource audio;

	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource> ();

		flagAnimator = GetComponent<Animator> ();

		UpdateFlags ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameObject.activeSelf) {

			if (canMove) {
				if (Input.GetKeyDown (KeyCode.LeftArrow)) {

					flagAnimator.SetTrigger ("Right");

					cursor--;

					canMove = false;
				} else if (Input.GetKeyDown (KeyCode.RightArrow)) {

					flagAnimator.SetTrigger ("Left");

					cursor++;
					canMove = false;

				} else if (Input.GetKeyDown (KeyCode.Space)) {

					SelectLangage ();
					mainMenuAnimator.SetTrigger ("space");
					flagAnimator.enabled = false;
				}
			}

			demo.text = demo.GetComponent<Translation> ().GetTranslation (GetLangage ());

		}
	}


	public void UpdateFlags()
	{

		int index;

		selectionPanel [0].texture = flags [(cursor + 10) % 6];

		selectionPanel [1].texture = flags [(cursor + 11) % 6];

		selectionPanel [2].texture = flags [(cursor + 12) % 6];

		selectionPanel [3].texture = flags [(cursor + 13) % 6];

		selectionPanel [4].texture = flags [(cursor + 14) % 6];


		if (cursor == -6 && cursor == 6)
			cursor = 0;
		
		canMove = true;

	}

	void SelectLangage()
	{
		LangageKeeper.langageKeeper.langage = GetLangage ();
	}


	Langage GetLangage()
	{
		int index = (cursor + 12) % 6;
		switch (index) {
		case 0:
			return Langage.English;
		case 1:
			return Langage.Francais;
		case 2:
			return Langage.Italien;
		case 3:
			return Langage.Espagnol;
		case 4:
			return Langage.Portugais;
		case 5:
			return Langage.Allemand;
		default :
			return Langage.English;
		}

	}


	public void playRotSound()
	{
		audio.Play ();
	}

}
