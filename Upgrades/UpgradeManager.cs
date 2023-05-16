using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int FriendlyNPCCount = 0;
    NPCSpawner _spawner;
    public List<FriendlyNPC> NPCS { get { return _npcs; } }
    List<FriendlyNPC> _npcs = new List<FriendlyNPC>();
    public float BaseHealth;
    PlayerHealth _health;
    public event Action<List<FriendlyNPC>> NPCCountChangeHandler;
    public void SetMaxHealth(float health)
    {
        BaseHealth = health;
        _health.UpdateMaxHealth(health);
    }
    private void Awake()
    {
        _spawner = FindObjectOfType<NPCSpawner>();
        _health = FindObjectOfType<PlayerHealth>();
    }
    public void InitListAfterLoad()
    {
        foreach (FriendlyNPC npc in FindObjectsOfType<FriendlyNPC>())
        {
            _npcs.Add(npc);
        }
        NPCCountChangeHandler.Invoke(_npcs);
    }
    public void AddToList(FriendlyNPC npc)
    {
        _npcs.Add(npc);
        FriendlyNPCCount++;
        NPCCountChangeHandler.Invoke(_npcs);
    }
    public void RemoveFromList(FriendlyNPC npc)
    {
        _npcs.Remove(npc);
        FriendlyNPCCount--;
        NPCCountChangeHandler.Invoke(_npcs);
    }
    public void UpdateText()
    {
        //TODO: Update Passive Income Text
    }
    public void SpawnWorker(int level)
    {
        StartCoroutine(SpawnCoroutine(level));
    }
    IEnumerator SpawnCoroutine(int level)
    {
        FriendlyNPC npc = _spawner.Spawn(0, FriendlyNPCCount);
        FriendlyNPCCount++;
        _npcs.Add(npc);
        NPCCountChangeHandler.Invoke(_npcs);
        npc.GetComponentInChildren<Animator>().Play("RUN");
        yield return new WaitForSeconds(ConstantValues.NPC_SPAWN_RATE);
    }
    public void OnMergeBuilders(List<FriendlyNPC> npcs)
    {
        Vector3 midPoint = npcs[2].transform.position;
        List<FriendlyNPC> sortedList = new List<FriendlyNPC>();
        sortedList = npcs.OrderBy(x => Vector3.Distance(x.transform.position, midPoint)).ToList();
        FriendlyNPC[] buildersToMerge = new FriendlyNPC[3] { sortedList[0], sortedList[1], sortedList[2] };

        for (int i = 0; i < buildersToMerge.Length; i++)
            buildersToMerge[i].Merge(midPoint);
    }
}




// while (npcs.Count > 2)
// {
//     BuilderNPC npc1 = npcs[0];
//     float dist = Mathf.Infinity;
//     BuilderNPC closestNPC = null;

//     for (int i = 1; i < npcs.Count; i++)
//     {
//         float curDist = Vector3.Distance(npc1.transform.position, npcs[i].transform.position);
//         if (curDist < dist)
//         {
//             closestNPC = npcs[i];
//             dist = curDist;
//         }
//     }

//     Vector3 midPoint = (npc1.transform.position + closestNPC.transform.position) / 2f;

//     List<GameObject> objs = new List<GameObject>(npc1.GetComponent<BuilderStats>().BuildList);
//     objs.AddRange(closestNPC.GetComponent<BuilderStats>().BuildList);

//     npc1.Merge(midPoint, () =>
//     {
//         _spawner.Spawn(npc1.Tier + 1, midPoint, objs);
//         npc1.GetComponent<BuilderFX>().PlayMergeFX();
//         Destroy(npc1.gameObject, 0.1f);
//     });
//     closestNPC.Merge(midPoint, () => Destroy(closestNPC.gameObject, 0.1f));

//     npcs.Remove(npc1);
//     npcs.Remove(closestNPC);
// }
