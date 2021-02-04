using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject spaceBarSound;

	Animator animator;

	bool waitForSpace = false;
	public void WaitForSpace()
	{
		waitForSpace = true;
	}

	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (waitForSpace) {
			if (Input.GetKeyDown (KeyCode.Space)) {

				animator.SetTrigger ("space");
				Instantiate (spaceBarSound, transform);

			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	public void LoadMainScene()
	{
		
		SceneManager.LoadScene ("Main");
	}
}
