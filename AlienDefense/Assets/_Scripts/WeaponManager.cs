using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
	PlayerGun[] playerGuns;
	PlayerGun currentGun;
	public PlayerGun GetCurrentGun { get { return currentGun; } }
	public int currentGunIndex = 0;

	public void Start()
	{
		playerGuns = GetComponentsInChildren<PlayerGun> ();
		currentGun = playerGuns [0];
		ChangeWeapon (0);
		currentGun.UpdateUI ();
	}

	public void ChangeWeapon(int i)
	{
		//if (currentGun != null && currentGun.CanAttack)
		{
			if (i < playerGuns.Length && i > -1) {
				currentGunIndex = i;
				if (currentGun == playerGuns [i])
					currentGun.UpdateUI (UI_ANIM_AMMO.EMPTY);
				else {
					Camera.main.GetComponent<Animator> ().SetInteger ("GunNum", i);
					SwapGun.SwapGunModels (i);
					PlayerUI.ChangeWeapon (i);
					currentGun = playerGuns [i];
					currentGun.UpdateUI (UI_ANIM_AMMO.SWITCH);
				}
			}
		}
	}

	public void Shoot()
	{
		currentGun.Shoot ();
	}

	public void AltShoot()
	{
		currentGun.AltShoot ();
	}

	public void AddAmmo(int ammo)
	{
		currentGun.AddAmmo (ammo);
	}
}
