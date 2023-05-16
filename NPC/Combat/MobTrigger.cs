using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTrigger : MonoBehaviour
{
    MobNPC _npc;
    private void Awake()
    {
        _npc = GetComponent<MobNPC>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _npc.GetToPos(other.transform.position, () => _npc.Attack());
    }
}
