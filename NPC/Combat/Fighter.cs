using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] protected float _damage;
    protected Health _currentTarget;
    protected Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void Attack(Health target)
    {
        _currentTarget = target;
        _animator.SetBool("Attack", true);
    }
    public void Damage()
    {
        if (_currentTarget != null)
            _currentTarget.TakeDamage(_damage, () => CheckCanAttack());
        else
        {
            CheckCanAttack();
        }
    }
    public void CheckCanAttack()
    {
        if (_currentTarget == null)
            _animator.SetBool("Attack", false);
    }
}
