using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageAble : MonoBehaviour
{
	// TODO EXPAND THIS AND MAKE IT BETTER
	public int MaxHealth = 1;

	public UnityEvent OnZeroHealthEvent = new UnityEvent();
	public UnityEvent OnDamageTakenEvent = new UnityEvent();

	// So that the ZeroHealthEvent wont keep firing every frame
	private bool alive = true;

	private int currentHealth;

	public int GetCurrentHealth()
	{
		return currentHealth;
	}

	private void Start()
	{
		currentHealth = MaxHealth;
		OnDamageTakenEvent.Invoke();
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		OnDamageTakenEvent.Invoke();

		if (currentHealth <= 0)
		{
			OnZeroHealthEvent.Invoke();
			alive = false;
		}
	}
}
