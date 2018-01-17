using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health {

	protected override void Start ()
	{
		healthMax *= OptionsSettings.playerHP_Mult;
		health = healthMax;
		PlayerUI.UpdateHealthText (health, healthMax);
	}

	public override void TakeDamage (float damage)
	{
		if (health <= 0f)
			return;
		PlayerUI.AnimateHit ();
		base.TakeDamage (damage);
		PlayerUI.UpdateHealthText (health, healthMax, UI_ANIM_HP.HIT);
	}


	public override void TakeDamage (float damage,Transform other)
	{
		if (health <= 0f)
			return;
		
		PlayerUI.AnimateHit (Vector3.SignedAngle(transform.forward,new Vector3(other.position.x,transform.position.y,other.position.z)-transform.position,transform.up));
		base.TakeDamage (damage);
		PlayerUI.UpdateHealthText (health, healthMax, UI_ANIM_HP.HIT);
	}

	public virtual void TakeDamage(float damage, Rigidbody body)
	{
		if (health <= 0f)
			return;

		PlayerUI.AnimateHit (Vector3.SignedAngle(transform.forward,-body.velocity-transform.position,transform.up));
		base.TakeDamage (damage);
		PlayerUI.UpdateHealthText (health, healthMax, UI_ANIM_HP.HIT);
	}

	void Update()
	{
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (Input.GetKey (KeyCode.RightShift)) {
				if (Input.GetKeyDown (KeyCode.R)) {
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
					SceneManager.LoadScene ("_MenuScene");
				}
			}
		}
	}
	public override void Die ()
	{
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<FPS_Controls> ().enabled = false;
		GetComponentInChildren<Animator> ().applyRootMotion = false;
		GetComponentInChildren<Animator>().SetTrigger ("Die");
	}


	public override void Heal (float _health)
	{
		base.Heal (_health);
		PlayerUI.UpdateHealthText (health, healthMax, UI_ANIM_HP.HEAL);
	}

}
