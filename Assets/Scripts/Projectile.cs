using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
	// TODO: MAKE THIS FANCY AND USE A SCRIPTABLE OBJECT FOR DATA
	[SerializeField] private GameObject hitParticlesPrefab;

	[SerializeField] private int damage;

	[SerializeField] private float speed;
	[SerializeField] private float lifetime;

	private float timer = 0;

	private void Start()
	{
		Rigidbody myRigidbody = GetComponent<Rigidbody>();
		myRigidbody.isKinematic = false;
		myRigidbody.velocity += this.transform.forward * speed;

		GetComponent<Collider>().isTrigger = true;
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer >= lifetime)
		{
			onHitParticles(this.transform.position);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Projectile"))
		{
			return;
		}

		DamageAble enemyHealth = other.gameObject.GetComponent<DamageAble>();
		Debug.Log("Dealing: " + damage + " dmg to: " + other.gameObject.name + " , healthscript found: " + enemyHealth);
		if (enemyHealth != null)
		{
			enemyHealth.TakeDamage(damage);
		}

		onHitParticles(this.transform.position - this.transform.forward);
	}

	private void onHitParticles(Vector3 pointOfImpact)
	{
		Instantiate(hitParticlesPrefab, pointOfImpact, this.transform.rotation, this.transform.parent);
		Destroy(this.gameObject);
	}
}
