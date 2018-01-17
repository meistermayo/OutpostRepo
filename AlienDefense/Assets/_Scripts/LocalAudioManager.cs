using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LocalAudioManager : MonoBehaviour {
	[SerializeField] AudioClip[] clips;
	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	public void Play(int a)
	{
		if (a > -1 && a < clips.Length)
		{
			if (audioSource != null) {
				audioSource.clip = clips [a];
				audioSource.Play ();
			} else {
				audioSource = GetComponent<AudioSource> ();
			}
		}
	}
}
