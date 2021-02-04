#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof( Translation ) )]
public class TranslationEditor : Editor {

	Text t;
	public Text text
	{
		get {
			if (t == null)
				t = GameObject.Find( "Text Up").GetComponent<Text>();
			return t;
		}
	}

	public override void OnInspectorGUI ()
	{
		Translation myObject = (Translation)target;
		if (t == null) {
			if (myObject.gameObject.GetComponent<Text> () != null)
				t = myObject.gameObject.GetComponent<Text> ();
		}


		EditorGUILayout.LabelField ("english translation");

		myObject.english = EditorGUILayout.TextArea( myObject.english );
		if ( GUILayout.Button( "Preview" ) )
			text.text = myObject.english;

		EditorGUILayout.Space ();

		EditorGUILayout.LabelField( "francais translation");


		myObject.francais = EditorGUILayout.TextArea( myObject.francais );
		if ( GUILayout.Button( "Preview" ) )
			text.text = myObject.francais;
		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("italien translation");


		myObject.italien = EditorGUILayout.TextArea(myObject.italien );
		if ( GUILayout.Button( "Preview" ) )
			text.text = myObject.italien;
		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("espagnol translation");


		myObject.espagnol = EditorGUILayout.TextArea( myObject.espagnol );
		if ( GUILayout.Button( "Preview" ) )
			text.text = myObject.espagnol;
		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("portugais translation");

		myObject.portugais = EditorGUILayout.TextArea(myObject.portugais );
		if ( GUILayout.Button( "Preview" ) )
			text.text = myObject.portugais;
		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("allemand translation");

		myObject.allemand = EditorGUILayout.TextArea(myObject.allemand );
		if ( GUILayout.Button( "Preview" ) )
			text.text = myObject.allemand;


		if (GUI.changed) {
			EditorUtility.SetDirty (text.gameObject);
		}


		if (!myObject.name.Contains ("Translation")) {
			EditorGUILayout.Space ();
			if (GUILayout.Button ("TRANSLATE!", GUILayout.Height(30)))
				myObject.Translate ();
		}
	}
}
#endif