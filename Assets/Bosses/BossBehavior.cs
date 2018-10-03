using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageAble), typeof(Rigidbody), typeof(BasicEnemyMovement))]
public class BossBehavior : MonoBehaviour
{
    public GameObject AwakenEffect;
    public GameObject DeathEffect;

    private Transform target = null;
    private Transform[] playerTransforms;

    private BasicEnemySkillActivation skillActivation;

    private DamageAble damageable;
    private BasicEnemyMovement movement;
    private Collider collider;

    private float addedChanceToChangeTarget;

    private void Awake()
    {
        damageable = GetComponent<DamageAble>();
        damageable.enabled = false;

        movement = GetComponent<BasicEnemyMovement>();
        movement.enabled = false;

        collider = GetComponent<Collider>();
        collider.enabled = false;

        skillActivation = GetComponent<BasicEnemySkillActivation>();
        skillActivation.enabled = false;
    }

    private void Start()
    {
        GetComponent<DamageAble>().OnZeroHealthEvent.AddListener(playDeathEffect);

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
            pickNewTarget();
        }
        else
        {
            if (Random.Range(0, 100) + addedChanceToChangeTarget >= 100)
            {
                addedChanceToChangeTarget = 0;
                pickNewTarget();
            }
            else
            {
                addedChanceToChangeTarget += Time.deltaTime;
            }
        }
    }

    public void WakeUp()
    {
        GameObject go = Instantiate(AwakenEffect, this.transform.position, this.transform.rotation);

        damageable.enabled = true;
        movement.enabled = true;
        collider.enabled = true;
        skillActivation.enabled = true;
    }

    private void playDeathEffect()
    {
        Instantiate(DeathEffect, this.transform.position, this.transform.rotation, this.transform.parent);
        Destroy(this.gameObject);
    }

    private void pickNewTarget()
    {
        int targetIndex = Random.Range(0, playerTransforms.Length - 1);

        this.target = playerTransforms[targetIndex];
        movement.target = this.target;
        skillActivation.target = this.target;
    }
}