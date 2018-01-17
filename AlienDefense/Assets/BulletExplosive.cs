using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosive : Bullet {
	[SerializeField] GameObject explosion;
	[SerializeField] GameObject explosionPrefab;

	protected virtual void OnTriggerEnter(Collider other)
	{

		if (tag == null)
			tag = GetComponent<Id_Tag> ();

		Id_Tag  otherTag = other.gameObject.GetComponent<Id_Tag> ();
		if (otherTag != null)
		{
			if (otherTag.GetTag () != tag.GetTag ()) {

				Health health = other.gameObject.GetComponent<Health> ();
				if (health != null) {
					health.TakeDamage (damage, transform);
					if (tag.GetTag () == 0) {
						GameObject.FindGameObjectWithTag ("Hitmarker").GetComponent<Animator> ().SetTrigger ("Hit");
					}
					Explode ();
					Destroy (gameObject);
				}
			}
		}

		if (other.CompareTag ("Ground")) {
			Explode ();
			Destroy (gameObject);
			return;
		}
	}

	protected virtual void Explode()
	{
		Instantiate (explosion, transform.position, Quaternion.identity);
		Instantiate (explosionPrefab, transform.position, Quaternion.identity);
	}

}
