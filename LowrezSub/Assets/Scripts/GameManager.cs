using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour {

	public GameObject spaceBarSound;
	[Space()]

	public LeaderBoard leaderBoard;

	public Image orange;
	public Image yellow;


	public Text scoreText;
	public RectTransform pileOfCoin;
	public Animator coinPileAnimator;

	public int score = 0;

	int plusScore;

	public GameObject scoreCanvas;
	public Text answerScreen;
	public Text answerScreen2;


	public int livesCursor = 2;

	public Text healthText;
	public Animator[] heartAnimator;

	public Langage langage
	{
		get {
			return LangageKeeper.langageKeeper.langage;
		}
	}

	[Space()]

	public static GameManager gm;

	public Text[] texts;

	public RawImage rawImage;

	public Translation[] translations;

	public Animator animator;

	string answer;

	[SerializeField]
	Text[] nTexts;

	string mainPath;

	bool canGuess = false;
	public bool waitForSpace;


	public void PlaySound( GameObject sound)
	{
		Instantiate (sound, transform);
	}


	public void UpadteLeaderBoard()
	{
		leaderBoard.UpdateLeaderBoard ();
	}

	public void WaitForSpace()
	{
		waitForSpace = true;
	}


	public void CanGuess()
	{
		canGuess = true;
	}

	void Awake()
	{
		if (gm == null)
			gm = this;
		else if (gm != this)
			Destroy (gameObject);
	}

	void Start () {


		//DebugLogAllCategories ();

		mainPath = Application.dataPath;

		animator = GetComponent<Animator>();


		RandomImage ();

		healthText.text = (livesCursor + 1).ToString ();


	}

	public void DisableDestroyedHearts()
	{
		heartAnimator [livesCursor + 1].gameObject.SetActive (false);
	}

	public void UpdateHealth()
	{

		heartAnimator [livesCursor].SetBool ("Destroyed", true);

		healthText.text = livesCursor.ToString();

		if (livesCursor == 0) {
			
			animator.SetBool ("GameOver", true);

		} else {
			livesCursor--;

			//healthText.text = livesCursor.ToString () + healthText.text.Substring (1, healthText.text.Length - 2);


		}
	}

	IEnumerator UpdateScore(int newScore)
	{

		int nb_of_coinGo = (int)((plusScore - 6) / 7) + 1;


		coinPileAnimator.SetBool ("Vibrate", true);
		int temp = score;

		float iterations = 20f;

		for (float i = 0f; i < 1f; i += 1f / iterations) {
			
			score = (int)((1 - i) * temp + i * newScore);

			scoreText.text = score.ToString ();

			pileOfCoin.anchoredPosition = new Vector2 (pileOfCoin.anchoredPosition.x, (int)(64f * score / 500f));

			yield return new WaitForSeconds (animator.GetFloat("coinSpeed") * nb_of_coinGo / iterations);

		}

		coinPileAnimator.SetBool ("Vibrate", false);
		animator.SetTrigger ("endScore");

	}


	public void TurnScoreOn()
	{
		scoreCanvas.SetActive (true);
		StartCoroutine (UpdateScore (score + plusScore));

	}


	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("MainMenu");
		}


		if (waitForSpace) {
			if (Input.GetKey (KeyCode.Space)) {
				waitForSpace = false;
				animator.SetTrigger ("space");
				Instantiate (spaceBarSound, transform);

			}
		} else {
			if (canGuess) {
				if (Input.GetKeyDown (KeyCode.UpArrow))
					Guess (0);
				if (Input.GetKeyDown (KeyCode.RightArrow))
					Guess (2);
				if (Input.GetKeyDown (KeyCode.LeftArrow))
					Guess (1);
			}
		}
		
	}

	void Guess( int i )
	{

		animator.SetBool ("InGame", true);

		string chosenAnswer = texts [i].text;
		if (chosenAnswer == answer) {

			plusScore = (int)(orange.fillAmount * 20f + 5);

			animator.SetTrigger ("goodAnswer");

		} else {

			//plusScore = -10;


			animator.SetTrigger ("wrongAnswer");
		}

		canGuess = false;

		/*
		 * 
		 * score
		 * à
		 * faire
		 * 
		 * 
		 * 
		 * 
		*/



	}

	void DebugLogAllCategories()
	{
		



		string[] categories = Directory.GetDirectories (Application.dataPath + "/Resources");
		string test = "";
		foreach (string category in categories) {

			answer = category.Substring (category.LastIndexOf ("\\") + 1, - category.LastIndexOf ("\\") - 1 + category.Length);




			//answer = answer.Replace ("_", " ");

			GameObject t = new GameObject();
			t.AddComponent<Translation>();
			t.name = answer;


			test += answer + "\n";

		}

		Debug.Log (test);
	}


	void ShuffleTexts ()
	{
		nTexts = new Text[3];
		if (Random.value < 0.333f) {

			nTexts [0] = texts [0];

			if (Random.value < 0.333f) {
				nTexts [1] = texts [1];
				nTexts [2] = texts [2];
			} else {
				nTexts [1] = texts [2];
				nTexts [2] = texts [1];
			}

		} else if (Random.value >= 0.333f && Random.value < 0.666f) {
			nTexts [1] = texts [0];

			if (Random.value < 0.333f) {
				nTexts [0] = texts [1];
				nTexts [2] = texts [2];
			} else {
				nTexts [0] = texts [2];
				nTexts [2] = texts [1];
			}

		} else {
			nTexts [2] = texts [0];

			if (Random.value < 0.333f) {
				nTexts [1] = texts [1];
				nTexts [0] = texts [2];
			} else {
				nTexts [1] = texts [2];
				nTexts [0] = texts [1];
			}
		}

	}

	int RandomIndex (int exceptAnswer, int except2 = -1)
	{
		int wrongAnswer = exceptAnswer;

		while( wrongAnswer == exceptAnswer || wrongAnswer == except2 )
			wrongAnswer = Random.Range (0, 87 - 1);

		return wrongAnswer;

	}


	public void RandomImage()
	{


		ShuffleTexts ();


		string categoriesText = ((TextAsset)Resources.Load ("categories")).text;

		int temp_Index = Random.Range (0, 86);

		int n = 0;
		int first = 0;
		int end = 0;

		for (int i = 0; i < categoriesText.Length - 2; i++) {

			if (categoriesText.Substring (i, 1) == "\n") {
				n++;
				if (n == temp_Index) {
					first = i + 1;
				} else if (n == temp_Index + 1) {
					end = i;
					break;
				}

			}
		}


		string rawAnswer = categoriesText.Substring (first, end - first);

		Debug.Log (rawAnswer + "     " + n);

		int answerIndex = n - 1;

		answer = translations [answerIndex].GetTranslation ();



		nTexts [0].text = answer;

		string victoryAnswer = answer;
		victoryAnswer = victoryAnswer.Replace ("-", "").Replace ("\n", "");


		answerScreen.text = victoryAnswer;
		answerScreen2.text = victoryAnswer;


		int wrong1 = RandomIndex (answerIndex);
		nTexts [1].text = translations[wrong1].GetTranslation();

		int wrong2 = RandomIndex (answerIndex, wrong1);
		nTexts [2].text = translations[wrong2].GetTranslation();


		string path = rawAnswer;

		Object[] preTextures = Resources.LoadAll (path);

		rawImage.texture = (Texture)preTextures [Random.Range (0, preTextures.Length - 1)];


		//rawImage.texture = answerTexture;

		/*
		string[] categories = Directory.GetDirectories (mainPath + "/ImageDataBank_64x64_checked");



		int answerIndex = Random.Range (0, categories.Length - 1);



		string temp = categories [answerIndex];


		//answer = temp.Substring (temp.LastIndexOf ("\\") + 1, - temp.LastIndexOf ("\\") - 1 + temp.Length);
		//answer = answer.Replace ("_", " ");

		//
		answer = translations[ answerIndex ].GetTranslation();
		//
		string victoryAnswer = answer;
		victoryAnswer = victoryAnswer.Replace ("-", "").Replace ("\n", "");


		ShuffleTexts ();


		int wrong1 = RandomIndex (answerIndex);


		//temp = categories [wrong1];
		//temp = temp.Substring (temp.LastIndexOf ("\\") + 1, - temp.LastIndexOf ("\\") - 1 + temp.Length);
		//temp = temp.Replace ("_", " ");

		//
		nTexts [1].text = translations[wrong1].GetTranslation();
		//

		int wrong2 = RandomIndex (answerIndex, wrong1);


		//temp = categories [wrong2];
		//temp = temp.Substring (temp.LastIndexOf ("\\") + 1, - temp.LastIndexOf ("\\") - 1 + temp.Length);
		//temp = temp.Replace ("_", " ");

		//
		nTexts [2].text = translations[wrong2].GetTranslation();
		//

		string pathToImage = Directory.GetFiles (categories [answerIndex]) [Directory.GetFiles (categories [answerIndex]).Length - 1];

		pathToImage = pathToImage.Replace ("/", "\\");

		pathToImage = pathToImage.Substring (0, pathToImage.Length - 5);

		Texture texture = IMG2Sprite.instance.LoadTexture (pathToImage);

		rawImage.texture = texture;

		*/
	}

	public void Save(GameData gameData)
	{


		BinaryFormatter bf = new BinaryFormatter ();

		FileStream file = File.Create (Application.persistentDataPath + "/GameData.dat");

		bf.Serialize (file, gameData);

		file.Close ();
	}

	public GameData Load()
	{
		if (File.Exists (Application.persistentDataPath + "/GameData.dat")) {


			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/GameData.dat", FileMode.Open);

			GameData data = (GameData)bf.Deserialize (file);
			file.Close ();
			return data;
		} else {

			GameData data = new GameData ();

			data.persons = new List<string> ();
			data.scores = new List<int> ();

			return data;
		}
	}

}

[System.Serializable]
public class GameData
{
	public List<string> persons;
	public List<int> scores;
}


public enum Langage
{
	English,
	Francais,
	Allemand,
	Espagnol,
	Italien,
	Portugais
}