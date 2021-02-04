#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DeletePlayerPrefs : MonoBehaviour {

	[MenuItem("Edit/Reset Player Prefs")]
	public static void dpp()
	{
		PlayerPrefs.DeleteAll();
	}
}
#endif