using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapGun : MonoBehaviour {
	[SerializeField] GameObject[] gunsRef;
	static GameObject[] guns;
	void Start()
	{
		guns=gunsRef;
	}
	public static void SwapGunModels(int i)
	{
		for (int j = 0; j < guns.Length; j++) {
			guns [j].SetActive (false);
		}
		guns [i].SetActive (true);
	}
}
