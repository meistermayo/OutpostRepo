using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	[SerializeField] Vector3 rotateEulerAngles;
	[SerializeField] bool random;
	[SerializeField] float rotateSpeed;
	// Use this for initialization
	void Start () {
		if (random) {
			rotateEulerAngles = new Vector3 (Random.value,Random.value,Random.value);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation *= Quaternion.Euler (rotateEulerAngles * rotateSpeed);
	}
}
