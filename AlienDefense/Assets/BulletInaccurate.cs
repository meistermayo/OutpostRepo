using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInaccurate : Bullet {
	[SerializeField] float inaccuracy;

	protected virtual void Start ()
	{
		tag = GetComponent<Id_Tag> ();
		if (tag.GetTag () == 0) {
			damage *= OptionsSettings.playerDamage_Mult;
		} else 
			damage *= OptionsSettings.enemyDamage_Mult;
		transform.rotation *= Quaternion.Euler( new Vector3 (Random.value-.5f,Random.value-.5f,Random.value-.5f)*inaccuracy);
		body = GetComponent<Rigidbody> ();
		body.velocity = transform.forward * moveSpeed;
		Destroy (gameObject, 5f);
	}
}
