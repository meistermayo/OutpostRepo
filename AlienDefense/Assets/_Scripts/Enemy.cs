using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState{
	SEARCH=0,
	FOLLOW,
	STRAFE,
	SHOOT
}

public class Enemy : MonoBehaviour {
	protected NavMeshAgent navMeshAgent;
	protected Health health;
	protected Gun myGun;
	protected Rigidbody body;
	[SerializeField] protected float followSpeed, strafeSpeed;
	[SerializeField] protected float minDist;
	protected EnemyState enemyState = EnemyState.SEARCH;
	protected bool isShooting;
	protected float strafeDir=1f;
	[SerializeField] protected GameObject[] searchDestinationArray;
	protected int currentSearch;
	protected float dist;
	[SerializeField] protected float damage=33f;
	[SerializeField] int burst;

	protected bool canAttack=true;
	// Use this for initialization
	protected virtual void Start () 
	{
		damage *= OptionsSettings.enemyDamage_Mult;
		dist = Random.Range (0f, 5f);
		navMeshAgent = GetComponent<NavMeshAgent> ();
		navMeshAgent.acceleration = followSpeed;
		navMeshAgent.speed = followSpeed;
		navMeshAgent.angularSpeed = followSpeed;
		if (Random.value > .5f) {
			strafeDir = -strafeDir;
		}
		body = GetComponent<Rigidbody> ();
		health = GetComponent<Health> ();
		myGun = GetComponentInChildren<Gun>();
		searchDestinationArray = GameObject.FindGameObjectsWithTag ("Destination");
	}

	protected virtual void Update()
	{

		float saveY = body.velocity.y;
		body.velocity = new Vector3 (body.velocity.x, 0f, body.velocity.z);
		if (FPS_Controls.Instance.transform == null)
			return;
		if (enemyState == EnemyState.SEARCH) {
			transform.LookAt (transform.position + navMeshAgent.velocity);
			Search ();
		}
		else {
			transform.LookAt (new Vector3(FPS_Controls.Instance.transform.position.x,transform.position.y,FPS_Controls.Instance.transform.position.z));
			if (enemyState == EnemyState.FOLLOW)
				Follow ();
		}


		body.velocity += Vector3.up * saveY;
	}

	protected virtual void Search()
	{
		if (navMeshAgent.isOnNavMesh) {
			navMeshAgent.destination = searchDestinationArray [currentSearch].transform.position;
			if (Vector3.Distance (navMeshAgent.destination, transform.position) < dist) {
				dist = Random.Range (0f, 5f);
				currentSearch++;
				if (currentSearch >= searchDestinationArray.Length) {
					currentSearch = 0;
				}
			}
			RaycastHit hit;
			if (Physics.Raycast (transform.position, FPS_Controls.Instance.transform.position - transform.position, out hit, 100f)) {
				if (hit.collider.CompareTag ("Player")) {
					enemyState = EnemyState.FOLLOW;
					navMeshAgent.destination = transform.position;
				}
			}
		} else {
			if (Physics.Raycast (transform.position, Vector3.down, 1f)) {
				navMeshAgent.enabled = false;
				navMeshAgent.enabled = true;
			}
		}
	}

	protected virtual void Follow()
	{

		if (navMeshAgent.isOnNavMesh) {
			navMeshAgent.destination = FPS_Controls.Instance.transform.position;
			//body.velocity = Vector3.Normalize (FPS_Controls.Instance.transform.position - transform.position) * followSpeed;
			//body.velocity = new Vector3 (body.velocity.x, 0f, body.velocity.z);

			if (Vector3.Distance (transform.position, FPS_Controls.Instance.transform.position) <= minDist) {
				Shoot ();
			}
			RaycastHit hit;
			if (Physics.Raycast (transform.position, FPS_Controls.Instance.transform.position - transform.position, out hit, 100f)) {
				if (!hit.collider.CompareTag ("Player")) {
					enemyState = EnemyState.SEARCH;
					navMeshAgent.destination = searchDestinationArray [currentSearch].transform.position;
				}
			}
		} else {
			if (Physics.Raycast (transform.position, Vector3.down, 1f)) {
				navMeshAgent.enabled = false;
				navMeshAgent.enabled = true;
			}
		}
	}

	protected virtual void Strafe()
	{
		navMeshAgent.destination = transform.position + Vector3.right * strafeDir * strafeSpeed;
		if (Vector3.Distance (transform.position, FPS_Controls.Instance.transform.position) <= minDist)
			enemyState = EnemyState.FOLLOW;
		Shoot ();
	}

	protected virtual void Shoot()
	{
		if (!isShooting) {
			StartCoroutine (StartShoot ());
		}
	}

	protected virtual IEnumerator StartShoot()
	{
		isShooting = true;
		yield return new WaitForSeconds (.1f);
		enemyState = EnemyState.SHOOT;
		body.velocity = Vector3.zero;
		for (int i = 0; i < burst; i++) {
			yield return new WaitForSeconds (myGun.Cooldown);
			Quaternion saveRotation = transform.rotation;
			myGun.transform.LookAt (Camera.main.transform.position);
			myGun.AddAmmo (1);
			myGun.Shoot ();
			transform.rotation = saveRotation;
		}
		isShooting = false;

		yield return new WaitForSeconds (1.5f);
		float r = Random.value;
		if (r >= .5f) {
			enemyState = EnemyState.FOLLOW;
			if (r >= .75f) {
				strafeDir = -strafeDir;
			}
		} else {
			enemyState = EnemyState.FOLLOW;
		}

	}

	void OnCollisionEnter(Collision other)
	{
		if (canAttack) {
			if (other.collider.CompareTag ("Player")) {
				if (other.collider.GetComponent<Rigidbody> ().velocity.y < -20f)
					GetComponent<Health> ().TakeDamage (9999f);
				else
					other.collider.GetComponent<PlayerHealth> ().TakeDamage (damage,transform);

			}
		}
	}

	IEnumerator AttackDelay()
	{
		canAttack = false;
		yield return new WaitForSeconds (1f);
		canAttack = true;
	}
}
