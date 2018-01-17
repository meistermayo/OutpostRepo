using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id_Tag : MonoBehaviour {
	[SerializeField] private int tag;

	public void SetTag(int t)
	{
		tag = t;
	}

	public int GetTag()
	{
		return tag;
	}
}
