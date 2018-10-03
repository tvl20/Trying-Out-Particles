using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    [Range(0f, 0.5f)] public float DelayBetweenSkillActivations = 0f;

    [Tooltip("this offset is used together with the LOCAL transform, this means that this is effected by rotation")]
    public Vector3 OffsetFromSummonCircle;

    public Transform[] SummonOriginTransform;

    public UnityEvent OnCooldownTimeChangedEvent = new UnityEvent();

    public bool movementLockingSkill = false;
    public bool isActive = false;

    protected GameObject[] ActivationParticles;
    protected GameObject[] ForceStopParticles;
    protected float Cooldown;
    protected float UniqueEffectStartDelay;

    private IEnumerator currentSkillCoroutine = null;
    private float timer = 0;

    public abstract Sprite GetAvailableSkillSprite();
    public abstract Sprite GetUnavailableSkillSprite();

    public float GetMaxCooldown()
    {
        return Cooldown;
    }

    public float GetRemainingCooldown()
    {
        if (Cooldown <= timer)
        {
            return 0;
        }
        else
        {
            return Cooldown - timer;
        }
    }

    public void ToggleActivation()
    {
        if (!isActive && timer >= Cooldown)
        {
            isActive = true;
            Activate();
        }
        else if (isActive)
        {
            Debug.Log("BAIL! the skill was cancelled");
            isActive = false;
            ForceStop();
        }
    }

    private void Activate()
    {
        timer = 0;

        // Make sure none are active at the time just in case
        if (currentSkillCoroutine != null)
        {
            StopAllCoroutines();
        }
        currentSkillCoroutine = null;

        currentSkillCoroutine = ActivateSkillCoroutine();
        StartCoroutine(currentSkillCoroutine);
    }

    private void ForceStop()
    {
        if (currentSkillCoroutine == null)
        {
            return;
        }

        StopAllCoroutines(); // Make sure there are no more coroutines running
        currentSkillCoroutine = null;


        foreach (GameObject activationParticles in ActivationParticles)
        {
            ParticleSystem[] particleSystems = activationParticles.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem system in particleSystems)
            {
                system.Clear();
                system.Stop();
            }
        }

        foreach (GameObject forceStopParticle in ForceStopParticles)
        {
            ParticleSystem[] particleSystems = forceStopParticle.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem system in particleSystems)
            {
                system.Play();
            }
        }

        _UniqueStopEffect();
    }

    protected virtual void _UniqueActivationEffect(int activationParticleIndex){}

    protected virtual void _UniqueStopEffect(){}

    // In this method load the Cooldown, Activation Particles, Force Stop Particles and the UniqueEffectStartDelay
    protected abstract void _LoadSkillData();

    private void Start()
    {
        _LoadSkillData();
    }

    private void Update()
    {
        if (timer < Cooldown)
        {
            timer += Time.deltaTime;
            OnCooldownTimeChangedEvent.Invoke();
        }
    }

    private IEnumerator ActivateSkillCoroutine()
    {
        for (int i = 0; i < SummonOriginTransform.Length - 1; i++)
        {
            StartCoroutine(SingleSkill(i));
            yield return new WaitForSeconds(DelayBetweenSkillActivations);
        }

        // Wait for the last coroutine to finish before closing, so that it is still possible to cancel
        yield return StartCoroutine(SingleSkill(SummonOriginTransform.Length - 1));
        currentSkillCoroutine = null;

        // Shuffle the lists so that different places will fire first next time
        shuffleAttackSequence();

        // Reset flag so that the skill status is right
        isActive = false;
    }

    private IEnumerator SingleSkill(int index)
    {
        ParticleSystem[] activationSystems = ActivationParticles[index].GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem system in activationSystems)
        {
            system.Play();
        }

        yield return new WaitForSeconds(UniqueEffectStartDelay);
        _UniqueActivationEffect(index);
    }

    private void shuffleAttackSequence()
    {
        for (int i = SummonOriginTransform.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0,i);

            // Swap all the list the same way
            swapPositions(SummonOriginTransform, i, r);
            swapPositions(ActivationParticles, i, r);
        }
    }

    private void swapPositions(Object[] array, int pos1, int pos2)
    {
        Object tmp = array[pos1];
        array[pos1] = array[pos2];
        array[pos2] = tmp;
    }
}
