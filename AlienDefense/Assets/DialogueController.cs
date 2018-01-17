using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
	[SerializeField] string myMessage;
	[SerializeField] float messageSpeed;
	[SerializeField] float messageDelay;
	[SerializeField] bool messageOnAwake;

	static string messageToPrint;
	static bool startMessage;

	LocalAudioManager audioManager;
	Text myText;
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		myText = GetComponentInChildren<Text> ();
		audioManager = GetComponent<LocalAudioManager> ();
		if (messageOnAwake) {
			messageToPrint = myMessage;
			StartCoroutine (PlayMessage ());
		}
	}

	void Update()
	{
		if (startMessage) {
			StopAllCoroutines ();
			startMessage = false;
			StartCoroutine (PlayMessage ());
		}
	}

	IEnumerator PlayMessage()
	{
		myText.text = "";
		animator.SetBool ("onscreen", true);
		yield return new WaitForSeconds (1f);
		foreach(char letter in messageToPrint.ToCharArray())
		{
			myText.text += letter;
			audioManager.Play (0);
			yield return new WaitForSeconds (messageSpeed);
		}
		yield return new WaitForSeconds (messageDelay);
		animator.SetBool ("onscreen", false);
	}

	public static void StartMessage(string message)
	{
		messageToPrint = message;
		startMessage = true;
	}

}
