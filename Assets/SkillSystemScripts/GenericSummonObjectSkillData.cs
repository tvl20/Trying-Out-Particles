using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "New Summon Skill", menuName = "Summon Skill")]
public class GenericSummonObjectSkillData : GenericSkillData
{
    public GameObject SummonObjectPrefab;
}
