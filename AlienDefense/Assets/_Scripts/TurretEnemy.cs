using UnityEngine;
using System.Collections;

public class TurretEnemy : Enemy
{
	[SerializeField] int burstCount;
	[SerializeField] float shotDelay;
	[SerializeField] float burstDelay;
	protected override void Update ()
	{
		transform.LookAt (new Vector3(FPS_Controls.Instance.transform.position.x,transform.position.y,FPS_Controls.Instance.transform.position.z));
		Shoot ();
	}

	protected override IEnumerator StartShoot ()
	{
		isShooting = true;
		yield return new WaitForSeconds (1f + Random.value*2f);
		for (int i = 0; i < burstCount; i++) {
			Quaternion saveRotation = transform.rotation;
			myGun.transform.LookAt (Camera.main.transform.position);
			myGun.AddAmmo (1);
			myGun.Shoot ();
			transform.rotation = saveRotation;
			yield return new WaitForSeconds (shotDelay);
		}
		isShooting = false;

		yield return new WaitForSeconds (burstDelay);

	
	}
}

