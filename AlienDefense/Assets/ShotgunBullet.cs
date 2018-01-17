using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet {
	[SerializeField] int shrapNumber;
	[SerializeField] GameObject shrapPrefab;
	protected virtual void Start ()
	{
		tag = GetComponent<Id_Tag> ();
		if (tag.GetTag () == 0) {
			damage *= OptionsSettings.playerDamage_Mult;
		} else 
			damage *= OptionsSettings.enemyDamage_Mult;

		body = GetComponent<Rigidbody> ();
		body.velocity = transform.forward * moveSpeed;

		for (int i = 0; i < shrapNumber; i++) {
			
			GameObject temp = Instantiate (shrapPrefab, transform.position, transform.rotation) as GameObject;
			temp.GetComponent<Id_Tag> ().SetTag (tag.GetTag ());
		}
		Destroy (gameObject);
	}
}
