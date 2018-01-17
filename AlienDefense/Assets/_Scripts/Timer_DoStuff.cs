using UnityEngine;
using System.Collections;

public abstract class Timer_DoStuff : MonoBehaviour
{
	[SerializeField] float timeInSeconds;

	void Start()
	{
		StartCoroutine (Timer());
	}

	public IEnumerator Timer()
	{
		yield return new WaitForSeconds(timeInSeconds);
		DoStuff ();
	}

	public abstract void DoStuff ();
}

