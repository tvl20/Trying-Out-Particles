using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRemoveParticleSystems : MonoBehaviour {
	// TODO: ADD THINGS FOR DAMAGE
	private ParticleSystem[] _particleSystems;

	private void Start()
	{
		_particleSystems = GetComponentsInChildren<ParticleSystem>();
	}

	private void Update()
	{
		// TODO: USE PARTICLE SYSTEM CALLBACK FOR CHECKING IF ALL THE SYSTEMS ARE DONE
		int amountDone = 0;
		foreach (ParticleSystem system in _particleSystems)
		{
			if (!system.IsAlive()) amountDone++;
		}

		if (amountDone == _particleSystems.Length)
		{
			Destroy(this.gameObject);
		}
	}
}
