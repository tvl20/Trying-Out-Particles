using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
	[SerializeField] private bool RotateTowardsCamera = true;
	[SerializeField] private DamageAble damagealbAble;
	[SerializeField] private Slider healthBarUI;

	private void Start ()
	{
		damagealbAble.OnDamageTakenEvent.AddListener(healthChanged);
		healthChanged();
	}

	private void LateUpdate()
	{
		if (RotateTowardsCamera)
		{
			this.transform.LookAt(Camera.main.transform);
		}
	}

	private void healthChanged()
	{
		healthBarUI.value = (float) damagealbAble.GetCurrentHealth() / damagealbAble.MaxHealth;
	}
}
