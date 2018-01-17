using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	protected float health;
	[SerializeField] protected float healthMax;
	public float GetHealth { get { return health;}}
	public float GetMaxHealth { get { return healthMax;}}
	protected virtual void Start()
	{
		healthMax *= OptionsSettings.enemyHP_Mult;
		health = healthMax;
	}

	public virtual void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0f)
			Die ();
	}

	public virtual void TakeDamage(float damage, Transform other)
	{
		health -= damage;
		if (health <= 0f)
			Die ();
	}
	public virtual void TakeDamage(float damage, Rigidbody body)
	{
		health -= damage;
		if (health <= 0f)
			Die ();
	}

	public virtual void Die()
	{
		Destroy(gameObject);
	}


	public virtual void Heal(float _health)
	{
		health = Mathf.Clamp (health+_health,0f,healthMax);
	}
}
