using UnityEngine;
using System.Collections;

public class BulletRaycast : Bullet
{
	[SerializeField] 	float inaccuracy;
	[SerializeField]	float decayTime;
	[SerializeField]	float destroyTime;
	LineRenderer lineRenderer;
	protected override void Start ()
	{
		lineRenderer = GetComponent<LineRenderer> ();
		damage *= OptionsSettings.playerDamage_Mult;
		tag = GetComponent<Id_Tag> ();
		transform.rotation *= Quaternion.Euler (new Vector3 (Random.value, Random.value, Random.value).normalized * inaccuracy);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, 1000f)) {
			Health hitHealth = hit.collider.GetComponent<Health> ();
			if (hitHealth != null) {
				Id_Tag  otherTag = hit.collider.GetComponent<Id_Tag> ();
				if (otherTag == null)
					return;
				if (otherTag.GetTag() == tag.GetTag())
					return;
				hitHealth.TakeDamage(damage);
				if (tag.GetTag () == 0) {
					GameObject.FindGameObjectWithTag ("Hitmarker").GetComponent<Animator>().SetTrigger("Hit");
				}
			}
		}
		Destroy (gameObject, destroyTime);
	}

	void Update()
	{
		if (lineRenderer.startWidth > 0f) {
			lineRenderer.startWidth -= decayTime;
		} else
			lineRenderer.startWidth = 0f;
		lineRenderer.endWidth = lineRenderer.startWidth;
	}
}

