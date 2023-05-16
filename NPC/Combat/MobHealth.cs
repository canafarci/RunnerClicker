using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobHealth : Health
{
    private float reward = 500f;
    [SerializeField] ParticleSystem _moneyFX;
    [SerializeField] int _experience = 1;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Die()
    {
        if (!References.Instance.PlayerHealth.IsDead)
        {
            _moneyFX.Play();
            Resource.Instance.OnMoneyChange(reward);
            References.Instance.Stats.GainExperience(_experience);
        }
        GetComponent<NavMeshAgent>().speed = 0f;
        base.Die();
    }
}
