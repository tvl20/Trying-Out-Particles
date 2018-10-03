using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageAble), typeof(PlayerMovement), typeof(PlayerSkillActivation))]
public class PlayerController : MonoBehaviour
{
    public GameObject DeathEffect;
    public float dodgeCooldown = 0.3f;

    private PlayerMovement movement;
    private PlayerSkillActivation _skillActivation;
    private float dodgeCooldownTimer = 0;

    private void Start()
    {
        GetComponent<DamageAble>().OnZeroHealthEvent.AddListener(playDeathEffect);
        movement = GetComponent<PlayerMovement>();
        _skillActivation = GetComponent<PlayerSkillActivation>();
    }

    private void Update()
    {
        if (dodgeCooldown > dodgeCooldownTimer)
        {
            dodgeCooldownTimer += Time.deltaTime;
        }

        if (Input.GetButtonDown("Dodge"))
        {
            Debug.Log(dodgeCooldownTimer + " / " + dodgeCooldown);
            if (dodgeCooldown <= dodgeCooldownTimer)
            {
                movement.dodge = true;
                dodgeCooldownTimer = 0;
            }
        }

        movement.lockMovement = _skillActivation.SkillActive && _skillActivation.ActiveSkillLocksMovement; // if a skill is active lock the movement of the player
    }

    private void playDeathEffect()
    {
        Instantiate(DeathEffect, this.transform.position, this.transform.rotation, this.transform.parent);
        Destroy(this.gameObject);
    }
}
