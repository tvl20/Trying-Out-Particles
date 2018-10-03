using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageAble))]
public class Barrier : MonoBehaviour
{
	[Tooltip("This is the time the wall will stay up, insert -1 if it should stay indefinitely")]
	[SerializeField] private float lifetime;

	[SerializeField] private GameObject destroyedParticleEffects;

	private float timer = 0;

	// Use this for initialization
	private void Start ()
	{
		GetComponent<DamageAble>().OnZeroHealthEvent.AddListener(onBarrierDestroy);
	}
	
	// Update is called once per frame
	private void Update () {
		if (lifetime >= 0)
		{
			timer += Time.deltaTime;

			if (timer >= lifetime)
			{
				onBarrierDestroy();
			}
		}
	}

	private void onBarrierDestroy()
	{
		Instantiate(destroyedParticleEffects, this.transform.position, this.transform.rotation, this.transform.parent);
		Destroy(this.gameObject);
	}
}
