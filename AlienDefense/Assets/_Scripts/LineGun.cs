using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGun : Gun {
	
	protected GameObject prevBullet;

	public override void Shoot()
	{
		if (canAttack && ammo > 0) {
			ammo--;
			audioManager.Play (0);
			GameObject temp = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
			temp.GetComponent<Id_Tag> ().SetTag (tag.GetTag());
			if (prevBullet != null)
				temp.GetComponent<LineBullet> ().SetBullet (prevBullet);
			prevBullet = temp;
			StartCoroutine (StartCooldown (cooldown));
		}
	}
}
