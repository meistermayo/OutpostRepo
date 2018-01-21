using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Cam.Effects;
public class TestRetroSizeAffector : MonoBehaviour {
	[SerializeField] PlayerHealth playerHealth;
	[SerializeField] RetroSize retroSize;
	float h,v;

	void Start()
	{
		h = 1920f;
		v = 1080f;
	}

	void Update()
	{
		float percent;
		percent = (playerHealth.GetHealth / playerHealth.GetMaxHealth);
		h = percent < 1f ? 1920f * .5f : 1920f;
		v = percent < 1f ? 1080f * .5f : 1080f;
		h *= percent;
		v *= percent;
		if (h < 221f)
			h = 221f;
		if (v < 124f)
			v = 124f;
		retroSize.horizontalResolution = Mathf.RoundToInt(h);
		retroSize.verticalResolution = Mathf.RoundToInt( v );
	}
}
