using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun_Pounder : PlayerGun {
	[SerializeField] GameObject secondaryBullet;
	[SerializeField] int secondaryAmmoCost;
	[SerializeField] float secondaryCooldown;

	public override void AltShoot()
	{
		if (canAttack)
		{
			if (ammo > 0) {
				Camera.main.GetComponent<Animator> ().SetTrigger ("Shoot");
				ammo-=secondaryAmmoCost;
				if (ammo < 0)
					ammo = 0;
				audioManager.Play (0);
				GameObject temp = Instantiate (secondaryBullet, transform.position, transform.rotation) as GameObject;
				temp.GetComponent<Id_Tag> ().SetTag (tag.GetTag ());
				UpdateUI (UI_ANIM_AMMO.SHOOT);
				StartCoroutine (StartCooldown (secondaryCooldown));
				FPS_Controls.Instance.KickCameraUp (kickUp);
			} else {
				UpdateUI(UI_ANIM_AMMO.EMPTY);
				audioManager.Play (1);
				StartCoroutine (StartCooldown (secondaryCooldown));
			}
		}
	}
}
