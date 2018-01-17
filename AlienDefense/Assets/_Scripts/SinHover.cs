using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinHover : MonoBehaviour {
	[SerializeField] float depth;
	[SerializeField] float rate;
	float angle;

	Vector3 iniPosition;
	// Use this for initialization
	void Start () {
		iniPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		angle++;
		transform.position = iniPosition + Vector3.up * Mathf.Sin(angle * rate) * depth;
	}
}
