using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Text;

public class Translation : MonoBehaviour {

	public string english;
	public string francais;
	public string italien;
	public string espagnol;
	public string portugais;
	public string allemand;

	public string GetTranslation()
	{
		switch (LangageKeeper.langageKeeper.langage) {

		case Langage.English:
			return english;
		case Langage.Allemand:
			return allemand;
		case Langage.Francais:
			return francais;
		case Langage.Espagnol:
			return espagnol;
		case Langage.Italien:
			return italien;
		case Langage.Portugais:
			return portugais;
		default :
			return "";
		}
	}

	public string GetTranslation(Langage lang)
	{
		switch (lang) {

		case Langage.English:
			return english;
		case Langage.Allemand:
			return allemand;
		case Langage.Francais:
			return francais;
		case Langage.Espagnol:
			return espagnol;
		case Langage.Italien:
			return italien;
		case Langage.Portugais:
			return portugais;
		default :
			return "";
		}
	}

	void Start()
	{
		Text text = GetComponent<Text> ();
		if (text != null) {

			text.text = GetTranslation ();


		}
	}

	public int GetIndex( string englishTranslation )
	{
		string fileName = "D:\\UNITY_DATA\\Unity Projects\\LowrezSub\\Assets\\Translations\\Texts\\text_" + 1.ToString() + ".txt";

		int index = 0;

		int finalIndex = -1;


		try {
			 
			string line;
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();

					if (line != null && line != "")
					{
						index ++;
						if ( line == englishTranslation ) {
							finalIndex = index;
							english = line;
							break;
						}
					}
				}
				while (line != null);
				theReader.Close();
			}
		}
		catch (Exception e) {
			throw e;
		}

		return finalIndex;

	}

	public void Translate()
	{
		int index = GetIndex (gameObject.name);
		francais = PreTranslate (2, index);
		italien = PreTranslate (3, index);
		espagnol = PreTranslate (4, index);
		portugais = PreTranslate (5, index);
		allemand = PreTranslate (6, index);

		name += "Translation";


	}


	public string PreTranslate(int langageIndex, int index)
	{



		string fileName = "D:\\UNITY_DATA\\Unity Projects\\LowrezSub\\Assets\\Translations\\Texts\\text_" + langageIndex.ToString() + ".txt";

		string translation = "null";
		int i = 0;

		try {

			string line;
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();

					if (line != null && line != "")
					{
						i ++;
						if ( i == index ) {
							translation = line;
							break;
						}
					}
				}
				while (line != null);
				theReader.Close();
			}
		}
		catch (Exception e) {
			throw e;
		}

		return translation.ToLower();
	}
}
