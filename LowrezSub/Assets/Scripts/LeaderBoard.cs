using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour {

	public Text[] letters;

	public RectTransform underlines;

	public float delay;

	public int nameLength;

	[Space(10)]


	public Text[] persons;
	public Text[] scores;


	int[] alphabetIndexes;

	int cursor;

	string final;

	string alphabet = "abcdefghijklmnopqrstuvwxyz1234567890";

	float timer;

	bool done;

	int x = -11;

	public void UpdateLeaderBoard()
	{

		GameManager.gm.animator.SetBool ("InGame", false);

		GameData leaderBoard = GameManager.gm.Load ();

		int insertIndex = -1;

		for (int i = 0; i < 5; i++) {

			if (leaderBoard.scores.Count > i) {
				int score = leaderBoard.scores [i];

				if (GameManager.gm.score > score) {
					insertIndex = i;
					break;
				}
			} else {
				insertIndex = i;
				break;
			}
		}


		if (insertIndex > -1) {

			leaderBoard.persons.Insert (insertIndex, final);
			leaderBoard.scores.Insert (insertIndex, GameManager.gm.score);

			if (leaderBoard.persons.Count > 5) {
				leaderBoard.persons.RemoveAt (5);
				leaderBoard.scores.RemoveAt (5);
			}

			GameManager.gm.Save (leaderBoard);


		}


		for( int i = 0 ; i < 5 ; i ++ )
		{
			if (i < leaderBoard.persons.Count) {
				persons [i].text = leaderBoard.persons [i];
				scores [i].text = leaderBoard.scores [i].ToString ();
			} else {
				persons [i].text = "";
				scores [i].text = "";
			}
		}






	}




	// Use this for initialization
	void Start () {

		alphabetIndexes = new int[nameLength];
		done = false;
		cursor = 0;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!done) {
			if (timer + delay < Time.time) {
				if (Input.GetKey (KeyCode.DownArrow)) {
			
					alphabetIndexes [cursor]++;
					if (alphabetIndexes [cursor] > alphabet.Length - 1)
						alphabetIndexes [cursor] = 0;
					timer = Time.time;

				} else if (Input.GetKey (KeyCode.UpArrow)) {
			
					alphabetIndexes [cursor]--;
					if (alphabetIndexes [cursor] < 0)
						alphabetIndexes [cursor] = alphabet.Length - 1;
					timer = Time.time;
				}



			}

			letters [cursor].text = alphabet [alphabetIndexes [cursor]].ToString ();
			underlines.anchoredPosition = new Vector2 (cursor * 11 + x, underlines.anchoredPosition.y);

			if (Input.GetKeyDown (KeyCode.LeftArrow)) {

				cursor--;

				if (cursor < 0)
					cursor = 0;

			} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
				cursor++;

				if (cursor > nameLength)
					cursor = nameLength;
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				Done ();
			}

		}
		
	}

	void Done()
	{
		GameManager.gm.animator.SetTrigger ("space");
		done = true;


		final = alphabet [alphabetIndexes [0]].ToString ()
		+ alphabet [alphabetIndexes [1]].ToString ()
		+ alphabet [alphabetIndexes [2]].ToString ();
	}

}
