using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDownLoader : MonoBehaviour {


	public string search;

	public Text progress;

	// Use this for initialization
	void Start () 
	{

		StartCoroutine (Test ());
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	IEnumerator Test()
	{
		string adress = "https://fr.pinterest.com/search/pins/?q="+search+"&rs=typed&term_meta[]="+search+"%7Ctyped";

		WWW wwwHtml= new WWW (adress);

        Debug.Log(adress);


        Debug.Log("test1");

		Debug.Log(wwwHtml.progress);

		StartCoroutine (showProgress (wwwHtml));

		yield return wwwHtml;

		string textHtml = wwwHtml.text;

		int start = textHtml.IndexOf ("_m9 _20 _3p _2c");

		if (start > 0) {

			Debug.Log (start);

			int finish = textHtml.IndexOf (".jpg", start);

			Debug.Log (finish);

			string url = textHtml.Substring (start + "class=\"_m9 _20 _3p _2c\" src=\"".Length, finish - start + 4);

			Debug.Log (url);
		}
	}

	IEnumerator showProgress(WWW wwwHtml )
	{
		while (wwwHtml.progress < 1f) {
			progress.text = wwwHtml.progress.ToString();
			yield return new WaitForSecondsRealtime (0.1f);
		}
	}

}
