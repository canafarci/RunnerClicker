using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Queue<Health> Enemies { get { return _enemiesInTrigger; } }
    Queue<Health> _enemiesInTrigger = new Queue<Health>();
    MoveForward _mover;
    private void Awake()
    {
        _mover = GetComponent<MoveForward>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!(other.CompareTag("NPC") || other.CompareTag("BOSSNPC"))) return;
        OnNPCCountChange(other.GetComponent<Health>());
    }

    void OnNPCCountChange(Health health)
    {
        _enemiesInTrigger.Enqueue(health);

        if (_enemiesInTrigger.Count == 0)
            _mover.IsEngaging = false;
        else
            _mover.IsEngaging = true;
    }
}
