using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

	private static Text myText;
	public static Text text {get {return myText;}}
	object[] data;

	void Start()
	{
		myText = GetComponent<Text> ();
	}

	public static void SetText(string text)
	{
		myText.text = text;
	}

	public static void SetText(string label, object data)
	{
		myText.text = label;
		myText.text += data.ToString ();
	}

	public static void AddText(string text)
	{
		myText.text += text;
	}



}
