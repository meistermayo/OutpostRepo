using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LocalAudioManager))]
public class Gun : MonoBehaviour
{
	[SerializeField] protected GameObject bullet;
	[SerializeField] protected float cooldown;
	public float Cooldown {get{return cooldown;}}
	[SerializeField] protected int maxAmmo;
	protected int ammo;
	protected bool canAttack=true;
	public bool CanAttack{get{return canAttack;}}
	protected LocalAudioManager audioManager;
	protected Id_Tag tag;

	public int Ammo {get {return ammo;}}
	public int MaxAmmo {get {return maxAmmo;}}

	protected virtual void Start()
	{
		tag = GetComponentInParent<Id_Tag> ();
		ammo = maxAmmo;
		audioManager = GetComponent<LocalAudioManager> ();
	}

	public virtual void Shoot()
	{
		if (canAttack && ammo > 0) {
			ammo--;
			audioManager.Play (0);
			GameObject temp = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
			temp.GetComponent<Id_Tag> ().SetTag (tag.GetTag());
			StartCoroutine (StartCooldown (cooldown));
		}
	}


	public virtual void AltShoot()
	{
	}

	public virtual IEnumerator StartCooldown(float value)
	{
		canAttack = false;
		if (value > 0f)
			yield return new WaitForSeconds(value);
		canAttack = true;
	}

	public virtual void AddAmmo(int _ammo)
	{
		ammo = Mathf.Clamp (ammo + _ammo, 0, maxAmmo);
	}
}

