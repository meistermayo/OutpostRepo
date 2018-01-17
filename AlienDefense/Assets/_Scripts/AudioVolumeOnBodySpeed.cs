using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeOnBodySpeed : MonoBehaviour {
	[SerializeField] Rigidbody body;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (body.velocity.y < -10f)
			audioSource.volume = -body.velocity.y / 80f;
		else
			audioSource.volume = 0f;
	}
}
