using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {
	[SerializeField] GameObject explosionPrefab;
	bool isDead;
	public override void Die ()
	{
		if (!isDead) {
			isDead = true;
			Instantiate (explosionPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
			base.Die ();
		}
	}
}
