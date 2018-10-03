using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSkillActivation : MonoBehaviour
{
    [Tooltip("This array should not be longer or shorter than 4 skills or null values, otherwise it will be resized"), SerializeField]
    private Skill[] Skills = new Skill[4];

    public bool SkillActive
    {
        get { return lastCastSkill != -1; }
    }

    public bool ActiveSkillLocksMovement
    {
        get { return SkillActive && Skills[lastCastSkill].movementLockingSkill; }
    }

    [SerializeField] private int lastCastSkill = -1;

    private void Start()
    {
        // Make sure the Skills array is only 4 elements long
        if (Skills.Length > 4)
        {
            // Take the first four
            Skills = new Skill[] {Skills[0], Skills[1], Skills[2], Skills[3]};
        }
        else if (Skills.Length < 4)
        {
            // Take the elements from the original list and add empty spots
            Skill[] newSkills = new Skill[4];

            for (int i = 0; i < Skills.Length; i++)
            {
                newSkills[i] = Skills[i];
            }

            Skills = newSkills;
        }
    }

    private void Update()
    {
        if (lastCastSkill == -1 || !Skills[lastCastSkill].isActive)
        {
            lastCastSkill = -1;

            for (int i = 1; i < 5; i++)
            {
                if (checkKey("Fire" + i, i - 1)) return;
            }
        }
        else
        {
            if (Input.GetButtonDown("CancelSkill") || Input.GetButtonDown("Fire" + (lastCastSkill + 1)) || Input.GetButtonDown("Dodge"))
            {
                Skills[lastCastSkill].ToggleActivation();
                lastCastSkill = -1;
            }
        }
    }

    // Returns the result of whether or not the skill has been activated
    private bool checkKey(String key, int skill)
    {
        if (Skills[skill] == null || !Input.GetButtonDown(key))
        {
            return false;
        }

        Skills[skill].ToggleActivation();

        if (Skills[skill].isActive)
        {
            lastCastSkill = skill;
            return true;
        }
        lastCastSkill = -1;
        return false;
    }
}