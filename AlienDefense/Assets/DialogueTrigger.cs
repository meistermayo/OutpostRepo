using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
	[TextArea(3,8)]
	[SerializeField] string message;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player"))
			TriggerDialogue ();
	}

	void TriggerDialogue()
	{
		DialogueController.StartMessage (message);
		Destroy (gameObject);
	}
}
