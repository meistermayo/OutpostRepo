using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
	[SerializeField] float depth;
	[SerializeField] float momentumThreshold;
	Rigidbody body;
	Vector3 shakePos;
	Vector3 iniPos;
	// Use this for initialization
	void Start () {
		iniPos = transform.localPosition;
		body = GetComponentInParent<Rigidbody> ();
		StartCoroutine (DoCameraShake ());
	}


	IEnumerator DoCameraShake()
	{
		while (true) {
			float absY = Mathf.Abs (body.velocity.y);
			if (absY < momentumThreshold) {
				transform.localPosition = iniPos;
				yield return null;
				continue;
			}

			shakePos = Random.insideUnitSphere;
			transform.localPosition = iniPos + shakePos * depth*absY;
			yield return null;

		}
	}
}
