using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DropShipState{
	LerpToPoint,
	DropEnemies,
	LerpToReturn
}

public class DropShip_Controller : MonoBehaviour {
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] float lerpSpeed;
	[SerializeField] Transform[] points;
	[SerializeField] Transform returnPoint;
	[SerializeField] int type;
	[SerializeField] int enemyMin,enemyMax;
	[SerializeField] bool randomEnemyCount;
	[SerializeField] float dropPosVariance;
	[SerializeField] GameObject tractorBeam;
	[SerializeField] float tractorBeamThickness;
	[SerializeField] float tractorBeamDecay;

	LocalAudioManager audioManager;
	int count;
	void Start()
	{
		if (type == 0) {
			if (!OptionsSettings.tanksOn)
				Destroy (gameObject);
		} else if (type == 1) {
			if (!OptionsSettings.fightersOn)
				Destroy (gameObject);
		} else if (type == 2) {
			if (!OptionsSettings.footmenOn)
				Destroy (gameObject);
		} else if (type == 3) {
			if (!OptionsSettings.turretsOn)
				Destroy (gameObject);
		}
		
		lerpSpeed *= OptionsSettings.dropshipSpeed_Mult;
		audioManager = GetComponent<LocalAudioManager> ();
		StartCoroutine (Route ());
	}

	void Update()
	{
		if (tractorBeam.activeSelf) {
			tractorBeam.transform.localScale = new Vector3 (tractorBeam.transform.localScale.x - tractorBeamDecay, tractorBeam.transform.localScale.y, tractorBeam.transform.localScale.z - tractorBeamDecay);
			if (tractorBeam.transform.localScale.x <= 0f) {
				tractorBeam.SetActive (false);
			}
		}
	}

	IEnumerator Route()
	{
		while (true) {
			// Go To Point
			{
				int point = Random.Range (0, points.Length);
				audioManager.Play (0);
				while (Vector3.Distance (transform.position, points [point].position) > 1f) {
					Vector3 last = transform.position;
					transform.position = Vector3.Lerp (transform.position, points [point].position,lerpSpeed);
					transform.rotation = Quaternion.LookRotation (transform.position - last);
					yield return new WaitForEndOfFrame ();
				}
			}

			// Spawn Enemies
			{
				int r = Random.Range (enemyMin, enemyMax);
				if (!randomEnemyCount)
					r = enemyMax;
				for (int i = 0; i < r; i++) {
					tractorBeam.SetActive (true);
					tractorBeam.transform.localScale = new Vector3 (tractorBeamThickness,tractorBeam.transform.localScale.y,tractorBeamThickness);
					Instantiate (enemyPrefab, transform.position + new Vector3 (Random.Range (-1f, 1f), 0f, Random.Range (-1f, 1f))*dropPosVariance, Quaternion.identity);
					audioManager.Play (1);
					yield return new WaitForSeconds (1f);
				}
				yield return new WaitForSeconds (2f);
			}

			// Retreat
			while (Vector3.Distance (transform.position, returnPoint.position) > 1f) {
				Vector3 last = transform.position;
				transform.position = Vector3.Lerp (transform.position, returnPoint.position,lerpSpeed);
				transform.rotation = Quaternion.LookRotation (transform.position - last);
				yield return new WaitForEndOfFrame ();
			}

			yield return new WaitForSeconds (Random.Range (1f, 3f));
			count++;
			if (count >= 5)
				Destroy (gameObject);
		}
	}
}
