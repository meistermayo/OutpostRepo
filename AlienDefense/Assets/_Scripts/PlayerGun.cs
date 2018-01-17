using UnityEngine;
using System.Collections;

public class PlayerGun : Gun
{
	[SerializeField] protected float kickUp;
	[SerializeField] protected float kickSide;

	protected override void Start ()
	{
		base.Start ();
		maxAmmo *= (int)OptionsSettings.playerAmmo_Mult;
		ammo = maxAmmo;
		//UpdateUI();
	}

	public override void AddAmmo (int _ammo)
	{
		base.AddAmmo (_ammo);
		UpdateUI(UI_ANIM_AMMO.EMPTY);
	}
	public override void Shoot ()
	{
		if (canAttack)
		{
			if (ammo > 0) {
				Camera.main.GetComponent<Animator> ().SetTrigger ("Shoot");
				ammo--;
				audioManager.Play (0);
				GameObject temp = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
				temp.GetComponent<Id_Tag> ().SetTag (tag.GetTag ());
				UpdateUI (UI_ANIM_AMMO.SHOOT);
				StartCoroutine (StartCooldown (cooldown));
				FPS_Controls.Instance.KickCameraUp (kickUp);
			} else {
				UpdateUI(UI_ANIM_AMMO.EMPTY);
				audioManager.Play (1);
				StartCoroutine (StartCooldown (cooldown));
			}
		}
	}

	public void UpdateUI()
	{
		PlayerUI.UpdateAmmoText (ammo, maxAmmo);
	}

	public void UpdateUI(UI_ANIM_AMMO anim)
	{
		PlayerUI.UpdateAmmoText (ammo, maxAmmo, anim);
	}
}

