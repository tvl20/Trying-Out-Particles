using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericSkillData : ScriptableObject
{
	// Wait for this duration before activating unique effect
	public float uniqueEffectStartDelay;

	public float Cooldown;

	public GameObject ActivationParticlesPrefab;
	public GameObject ForceStopParticlesPrefab;

	public Sprite AvailableSkillImage;
	public Sprite UnavailableSkillImage;
}
