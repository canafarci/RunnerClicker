using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool IsDead { get { return _currentHealth <= 0; } }
    [SerializeField] protected float _maxHealth;
    protected HealthUI _healthSlider;
    protected float _currentHealth;
    ActorFX _fx;
    protected virtual void Awake()
    {
        _healthSlider = GetComponent<HealthUI>();
        _currentHealth = _maxHealth;
        _fx = GetComponent<ActorFX>();
    }

    public virtual void TakeDamage(float damage, Action callback)
    {
        _fx.PlayHitFX();
        _currentHealth -= damage;
        _healthSlider.UpdateHealthSlider(_maxHealth, _currentHealth);
        if (_currentHealth <= 0)
        {
            callback();
            Die();
        }
    }

    protected virtual void Die()
    {
        GetComponentInChildren<Animator>().Play("Die");
        _healthSlider.DisableUI();
        Destroy(gameObject, 4f);
    }
}
