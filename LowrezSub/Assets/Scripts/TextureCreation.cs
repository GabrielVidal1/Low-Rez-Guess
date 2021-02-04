#if UNITY_EDITOR
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class TextureCreation : MonoBehaviour {

	static RenderTexture camPreview;


	static string path;
	static string masterPath = "D:\\IMAGES\\101_ObjectCategories";

	static string testPath;

	static Texture2D tex;

	static Image image;

	static int cursor;

	static float up;
	static float down;
	static float right;
	static float left;

	static int cursorPath = 0;

	static Camera cam;


	static bool saveAllImages = false;

	//static string imageCategory;

	static string[] masterDirectories;

	/*
	[MenuItem("Tools/TextureCreation")]
	static void Init () {
		TextureCreation window = (TextureCreation)GetWindow (typeof( TextureCreation ));
		window.Show ();
	}
	*/
	void Start()
	{
		StartCoroutine (Go ());

	}


	IEnumerator Go()
	{
		image = GameObject.Find ("TextureImage").GetComponent<Image> ();

		cam = Camera.main;
		camPreview = new RenderTexture (64, 64, 8);
		cam.targetTexture = camPreview;

		masterDirectories = Directory.GetDirectories (masterPath);


		for (int i = 0; i < masterDirectories.Length - 1; i++) {

			string path = masterDirectories [i];

			string[] files = Directory.GetFiles (path);

			string imageCategory = path.Substring (path.LastIndexOf ("\\") + 1, path.Length - 1 - path.LastIndexOf ("\\"));

			for (int j = 0; j < files.Length - 1; j++) {

				string imagePath = files [j];

				SaveImage (imagePath, j, imageCategory);

				yield return new WaitForSeconds (.01f);



			}
		}

		AssetDatabase.Refresh ();

		Debug.Log ("Done");
	}
	/*
	void OnGUI()
	{

		EditorGUILayout.LabelField (masterPath);



		EditorGUILayout.BeginHorizontal ();

		if (GUILayout.Button( "Reset", GUILayout.Width( 50 ) ) )
			cursorPath = 0;

		if (cursorPath > 0) {
			if (GUILayout.Button ("Previous"))
				cursorPath--;
		} else {
			EditorGUILayout.Space ();
		}
		if (cursorPath < masterDirectories.Length - 1) {
			if (GUILayout.Button ("Next"))
				cursorPath++;
		} else {
			EditorGUILayout.Space ();
		}


		EditorGUILayout.EndHorizontal ();

		//Debug.Log (cursorPath + "       " + masterDirectories.Length);

		path = masterDirectories [cursorPath];

		EditorGUILayout.LabelField ("Path", path);




		if (path == null || image == null) {


			//image = EditorGUILayout.ObjectField ("Image In Canvas", image, typeof(Image)) as Image;


			EditorGUILayout.HelpBox ("You must submit the path to the image directory", MessageType.Error);

		} else {

			if (cam == null || GUILayout.Button("Reset Camera")) {
				cam = Camera.main;
				camPreview = new RenderTexture (64, 64, 8);
				cam.targetTexture = camPreview;

			}

			imageCategory = path.Substring (path.LastIndexOf ("\\") + 1, path.Length - 1 - path.LastIndexOf ("\\"));

			EditorGUILayout.LabelField ("Image Category", imageCategory);


			EditorGUILayout.LabelField ("Image Name", imageCategory + "_" + cursor.ToString());

			string[] files = Directory.GetFiles (path);

			if (cursor > files.Length - 1) {
				cursor = 0;
				saveAllImages = false;
			}


			GUILayout.BeginHorizontal ();
			if (GUILayout.Button( "Reset", GUILayout.Width( 50 ) ) )
				cursor = 0;


			if (cursor > 0) {
				if (GUILayout.Button ("Previous"))
					cursor--;
			} else
				EditorGUILayout.Space ();
				
			if (cursor < files.Length - 1) {
				if (GUILayout.Button ("Next")) {
					cursor++;
				}
			} else {
				EditorGUILayout.Space ();
			}

			GUILayout.EndHorizontal();

			testPath = files [cursor];
			image.sprite = IMG2Sprite.instance.LoadNewSprite (testPath);

			EditorGUILayout.BeginVertical ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField (GUIContent.none);
			up = EditorGUILayout.Slider (GUIContent.none, up, -100f, 100f);
			EditorGUILayout.LabelField (GUIContent.none);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			left = EditorGUILayout.Slider (GUIContent.none, left, -100f, 100f);
			EditorGUILayout.LabelField (GUIContent.none);
			right = EditorGUILayout.Slider (GUIContent.none, right, -100f, 100f);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField (GUIContent.none);
			down = EditorGUILayout.Slider (GUIContent.none, down, -100f, 100f);
			EditorGUILayout.LabelField (GUIContent.none);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.EndVertical ();

			image.rectTransform.offsetMax = new Vector2 (right, up);
			image.rectTransform.offsetMin = new Vector2 (left, down);

			if (GUILayout.Button ("Save Image")) 
			{

				SaveImage (testPath, cursor);

				AssetDatabase.Refresh ();

			}


			if (saveAllImages) {
				SaveImage (testPath, cursor);

				cursor++;
			}



			if (GUILayout.Button ("Save all images", GUILayout.Height (30))) {
				cursor = 0;

				//saveAllImages = true;


				SaveAllImages (files);
			}
			
		}
	}
		*/
	/*
	void SaveAllImages(string[] files)
	{

		for (int t = 0; t < 5; t++) {


			image.sprite = IMG2Sprite.instance.LoadNewSprite (files[t]);
			Debug.Log (files [t]);

			SaveImage (files [t], t);

		}
		AssetDatabase.Refresh ();



	}
	*/
	static void ResetCursors()
	{
		cursor = 0;
		cursorPath = 0;
	}


	void SaveImage( string fileName, int index, string imageCategory )
	{
		//Debug.Log (fileName);

		//Debug.Log (IMG2Sprite.instance);

		image.sprite = IMG2Sprite.instance.LoadNewSprite (fileName);




		//camPreview = new RenderTexture (64, 64, 8);
		//cam.targetTexture = camPreview;

		Texture2D texture2d = new Texture2D(camPreview.width, camPreview.height);
		
		RenderTexture.active = camPreview;;
		
		texture2d.ReadPixels (new Rect (0, 0, camPreview.width, camPreview.height), 0, 0);
		texture2d.Apply ();
		
		byte[] bytes = texture2d.EncodeToPNG();
		Object.DestroyImmediate(texture2d);
		
		if ( !Directory.Exists ("Assets/ImageDataBank/" + imageCategory)) {
			Directory.CreateDirectory ("Assets/ImageDataBank/" + imageCategory);
		}

		//Debug.Log (index);
		//Debug.Log ("Assets/ImageDataBank/"+ imageCategory + "/" + imageCategory + "_" + index.ToString() + ".png");

		File.WriteAllBytes ("Assets/ImageDataBank/"+ imageCategory + "/" + imageCategory + "_" + index.ToString() + ".png", bytes);
		
	}


	/*
	IEnumerator LoadImage()
	{
		tex = new Texture2D (1024, 1024, TextureFormat.DXT1, false);


		WWW test = new WWW (testPath);
		yield return test;
		test.LoadImageIntoTexture (tex);


	}
*/
}
#endif