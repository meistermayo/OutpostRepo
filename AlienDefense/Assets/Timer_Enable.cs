using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Enable : Timer_DoStuff {
	[SerializeField] GameObject[] objectsToEnable;
	[SerializeField] bool enable=true;

	public override void DoStuff ()
	{
		foreach (GameObject go in objectsToEnable) {
			go.SetActive (enable);
		}
	}
}
