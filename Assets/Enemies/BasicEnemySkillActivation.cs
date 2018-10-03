using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemySkillActivation : MonoBehaviour
{
	public Transform target = null;

	[SerializeField]
	private Skill Skill;

	[SerializeField]
	private const float minimumWaitTimeBeforeSkillCancel = 1;
	private float timer;

	public bool SkillActive
	{
		get { return Skill.isActive; }
	}

	public bool ActiveSkillLocksMovement
	{
		get { return SkillActive && Skill.movementLockingSkill; }
	}

	private void Update()
	{
		if (target != null)
		{
			if (!Skill.isActive)
			{
				RaycastHit hit;
				if (Physics.Raycast(this.transform.position, this.transform.forward, out hit))
				{
					if (hit.transform.CompareTag("Player"))
					{
						timer = 0;
						Skill.ToggleActivation();
					}
				}
			}
			else
			{
				if (minimumWaitTimeBeforeSkillCancel > timer)
				{
					timer += Time.deltaTime;
					return;
				}

				RaycastHit hit;
				if (!Physics.Raycast(this.transform.position, this.transform.forward, out hit))
				{
					if (!hit.transform.CompareTag("Player"))
					{
						Skill.ToggleActivation();
					}
				}
			}
		}
	}
}
