using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobNPC : NavMeshNPC
{
    public GameObject PlayerDummy;
    Fighter _fighter;
    protected override void Awake()
    {
        base.Awake();
        _fighter = GetComponentInChildren<Fighter>();
    }
    private void Update()
    {

        _agent.SetDestination(PlayerDummy.transform.position);
    }
    public void Attack()
    {
        _fighter.Attack(References.Instance.PlayerHealth);
    }
}
