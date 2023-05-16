using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FriendlyNPC : NavMeshNPC
{
    public int Tier = 0;
    FriendlyNPCFighter _fighter;
    bool _isMerging = false;
    public Transform FollowTransform;
    Coroutine _followLoop;
    float _stopDist;
    protected override void Awake()
    {
        base.Awake();
        _fighter = GetComponentInChildren<FriendlyNPCFighter>();
        _stopDist = _agent.stoppingDistance;
    }
    private void Start()
    {
        PlayerFighter playerFighter = FindObjectOfType<PlayerFighter>();

        if (playerFighter.CurrentTarget != null && !playerFighter.CurrentTarget.IsDead)
        {
            MoveAndAttack(playerFighter.CurrentTarget);
        }
        else
        {
            _followLoop = StartCoroutine(FollowLoop());
        }
    }
    public void MoveAndAttack(Health target)
    {
        if (_isMerging) return;
        if (_followLoop != null)
            StopCoroutine(_followLoop);

        GetToPos(target.transform.position, () => _fighter.Attack(target));
    }
    public void MoveBack()
    {
        if (_isMerging) return;
        _fighter.CancelAttack();
        CancelMoveCoroutine();
        _followLoop = StartCoroutine(FollowLoop());
    }
    public void Merge(Vector3 midPoint)
    {
        _isMerging = true;

        Animator animator = GetComponentInChildren<Animator>();
        Action callback = () =>
        {
            FindObjectOfType<NPCSpawner>().Spawn(Tier + 1, animator);
        };

        if (_followLoop != null)
            StopCoroutine(_followLoop);

        GetToPos(midPoint, callback);
    }
    IEnumerator FollowLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            _agent.destination = FollowTransform.position;
            _agent.stoppingDistance = 0f;
        }
    }
    public override Coroutine GetToPos(Vector3 target, Action callback = null)
    {
        _agent.stoppingDistance = _stopDist;
        return base.GetToPos(target, callback);
    }
}
