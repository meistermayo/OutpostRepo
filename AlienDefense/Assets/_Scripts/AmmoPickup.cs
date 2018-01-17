using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup {
	[SerializeField] int value;
	protected override void ApplyEffect (GameObject pickerUpper)
	{
		pickerUpper.GetComponent<WeaponManager> ().AddAmmo (value);
	}

	protected override void RemoveEffect (GameObject pickerUpper)
	{
		// do nothing
	}

	protected override bool ExitPickup (GameObject pickerUpper)
	{
		return (pickerUpper.GetComponent<WeaponManager> ().GetCurrentGun.Ammo >= pickerUpper.GetComponent<WeaponManager>().GetCurrentGun.MaxAmmo);
	}
}
