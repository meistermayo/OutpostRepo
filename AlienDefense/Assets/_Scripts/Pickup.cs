using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour {
	[SerializeField] float duration;
	[SerializeField] float respawnTime;

	Collider collider;
	GameObject model;
	AudioSource audioSource;

	void Start()
	{
		respawnTime *= OptionsSettings.itemRespawn_Mult;
		collider = GetComponent<Collider> ();
		model = transform.GetChild (0).gameObject;
		audioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) {
			StartCoroutine (GetPickedUp (other.gameObject));
		}
	}

	IEnumerator GetPickedUp(GameObject pickerUpper)
	{
		if (!ExitPickup (pickerUpper)) {
			if (audioSource != null)
				audioSource.Play ();
			collider.enabled = false;
			model.SetActive (false);
			ApplyEffect (pickerUpper);

			yield return new WaitForSeconds (duration);
			yield return new WaitForSeconds (respawnTime - duration);

			RemoveEffect (pickerUpper);
			collider.enabled = true;
			model.SetActive (true);
		}
	}

	protected abstract void ApplyEffect(GameObject pickerUpper);

	//Can sometimes do nothing
	protected abstract void RemoveEffect(GameObject pickerUpper);

	protected virtual bool ExitPickup(GameObject pickerUpper)
	{
		return false;
	}
}
