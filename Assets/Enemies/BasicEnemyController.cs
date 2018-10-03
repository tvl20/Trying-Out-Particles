using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;

[RequireComponent(typeof(DamageAble), typeof(Rigidbody), typeof(BasicEnemyMovement))]
public class BasicEnemyController : MonoBehaviour
{
    public GameObject DeathEffect;
    public float AggroRange = 15;

    private Transform target = null;
    private Transform[] playerTransforms;

    private BasicEnemyMovement movement;
    private BasicEnemySkillActivation skillActivation;

    private void Start()
    {
        GetComponent<DamageAble>().OnZeroHealthEvent.AddListener(playDeathEffect);

        movement = GetComponent<BasicEnemyMovement>();
        skillActivation = GetComponent<BasicEnemySkillActivation>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        playerTransforms = new Transform[players.Length];
        for (int i = 0; i < playerTransforms.Length; i++)
        {
            playerTransforms[i] = players[i].transform;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            foreach (Transform player in playerTransforms)
            {
                float distanceToPlayer = Vector3.Distance(player.position, this.transform.position);
                if (distanceToPlayer <= AggroRange &&
                    (target == null || distanceToPlayer <= Vector3.Distance(target.position, this.transform.position)))
                {
                    target = player;
                    movement.target = this.target;
                    skillActivation.target = this.target;
                    break;
                }
            }
        }
        else
        {
            if (Vector3.Distance(target.position, this.transform.position) > AggroRange)
            {
                this.target = null;
                movement.target = null;
                skillActivation.target = null;
            }
        }

        movement.lockMovement = skillActivation.SkillActive && skillActivation.ActiveSkillLocksMovement;
    }

    private void playDeathEffect()
    {
        Instantiate(DeathEffect, this.transform.position, this.transform.rotation, this.transform.parent);
        Destroy(this.gameObject);
    }
}