using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup {
	[SerializeField] float value;
	protected override void ApplyEffect (GameObject pickerUpper)
	{
		pickerUpper.GetComponent<PlayerHealth> ().Heal(value);
	}

	protected override void RemoveEffect (GameObject pickerUpper)
	{
		// do nothing
	}

	protected override bool ExitPickup (GameObject pickerUpper)
	{
		return (pickerUpper.GetComponent<PlayerHealth> ().GetHealth >= pickerUpper.GetComponent<PlayerHealth>().GetMaxHealth);
	}
}
