using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerSkillUI : MonoBehaviour
{
	[SerializeField] private Skill skill;

	[SerializeField] private Image skillAvailableImage;
	[SerializeField] private Image skillUnavailableImage;

	[SerializeField] private Text skillCooldownText;

	void Start ()
	{
		if (skill == null)
		{
			return;
		}

		skill.OnCooldownTimeChangedEvent.AddListener(updateCooldownUI);

		skillAvailableImage.sprite = skill.GetAvailableSkillSprite();
		skillAvailableImage.type = Image.Type.Filled;

		skillUnavailableImage.sprite = skill.GetUnavailableSkillSprite();
		skillUnavailableImage.type = Image.Type.Filled;

		updateCooldownUI();
	}

	private void updateCooldownUI()
	{
		float remainingCooldown = skill.GetRemainingCooldown();
		if (remainingCooldown > 0)
		{
			if (!skillCooldownText.gameObject.activeSelf)
			{
				skillCooldownText.gameObject.SetActive(true);
			}

			skillUnavailableImage.fillAmount = remainingCooldown / skill.GetMaxCooldown();
			skillCooldownText.text = Math.Round(remainingCooldown, 1).ToString();
		}
		else
		{
			skillCooldownText.gameObject.SetActive(false);
		}
	}
}
