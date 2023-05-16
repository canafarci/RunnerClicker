using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FriendlyNPCFighter : Fighter
{
    Vector3 _startRot;
    protected override void Awake()
    {
        base.Awake();
        _startRot = transform.rotation.eulerAngles;
    }
    private void Update()
    {
        if (_currentTarget == null)
        {
            transform.LookAt(transform.parent.position + transform.parent.forward);
        }
        else
        {
            transform.LookAt(_currentTarget.transform);
        }
    }

    public void CancelAttack()
    {
        _currentTarget = null;
        _animator.SetBool("Attack", false);
        transform.DORotate(_startRot, 1f);
    }

}
