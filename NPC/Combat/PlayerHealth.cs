using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    [SerializeField] ParticleSystem _healthFX;
    bool _firstCall = true;
    bool _calledDeath = false;
    public void UpdateMaxHealth(float maxHealth)
    {
        if (_currentHealth == 0)
            _currentHealth = _maxHealth;

        float ratio = _currentHealth / _maxHealth;
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth * ratio;
        _healthSlider.UpdateHealthSlider(_maxHealth, _currentHealth);

        if (_firstCall)
            _firstCall = false;
        else
            _healthFX.Play();
    }
    protected override void Die()
    {
        if (_calledDeath) return;
        GetComponentInChildren<Animator>().Play("Die");
        _healthSlider.DisableUI();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneLoader.Instance.DelayedLoadScene(sceneIndex, 4f);
        _calledDeath = true;
    }
}
