using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Id_Tag))]
public class Bullet : MonoBehaviour
{
	protected Rigidbody body;
	protected Id_Tag tag;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float damage;

	// Use this for initialization
	protected virtual void Start ()
	{
		tag = GetComponent<Id_Tag> ();
		if (tag.GetTag () == 0) {
			damage *= OptionsSettings.playerDamage_Mult;
		} else 
			damage *= OptionsSettings.enemyDamage_Mult;
		
		body = GetComponent<Rigidbody> ();
		body.velocity = transform.forward * moveSpeed;
		Destroy (gameObject, 5f);
	}

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
					Destroy (gameObject);
				}
			}
		}

		if (other.CompareTag ("Ground")) {
			Destroy (gameObject);
			return;
		}
	}
}

