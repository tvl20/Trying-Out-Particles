using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleSummonObjectSkill : Skill
{
	[SerializeField] private GenericSummonObjectSkillData _skillData;

	public override Sprite GetAvailableSkillSprite()
	{
		return _skillData.AvailableSkillImage;
	}

	public override Sprite GetUnavailableSkillSprite()
	{
		return _skillData.UnavailableSkillImage;
	}

	protected override void _UniqueActivationEffect(int activationParticleIndex)
	{
		Vector3 summonPosition = getSummonPosition(SummonOriginTransform[activationParticleIndex]);

		Instantiate(_skillData.SummonObjectPrefab, summonPosition, SummonOriginTransform[activationParticleIndex].rotation);
	}

	protected override void _LoadSkillData()
	{
		ActivationParticles = getSystemsForAllTransforms(_skillData.ActivationParticlesPrefab);
		ForceStopParticles = getSystemsForAllTransforms(_skillData.ForceStopParticlesPrefab);
		Cooldown = _skillData.Cooldown * ((SummonOriginTransform.Length + SummonOriginTransform.Length * 0.1f) - 0.1f);
		UniqueEffectStartDelay = _skillData.uniqueEffectStartDelay;
	}

	private void Awake()
	{
		SummonOriginTransform = GetComponentsInChildren<Transform>();

		// The first one is always this transform, so we need to remove that
		SummonOriginTransform = SummonOriginTransform.Skip(1).ToArray();
	}

	private GameObject[] getSystemsForAllTransforms(GameObject particleSystemPrefab)
	{
		List<GameObject> output = new List<GameObject>();

		foreach (Transform pos in SummonOriginTransform)
		{
			output.Add(Instantiate(particleSystemPrefab, pos.position, pos.rotation, this.transform));
		}
		return output.ToArray();
	}

	private Vector3 getSummonPosition(Transform originPoint)
	{
		Vector3 output = originPoint.position;
		output += originPoint.right * OffsetFromSummonCircle.x;
		output += originPoint.up * OffsetFromSummonCircle.y;
		output += originPoint.forward * OffsetFromSummonCircle.z;
		return output;
	}
}
