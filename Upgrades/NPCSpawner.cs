using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] GameObject[] _prefabs;
    UpgradeManager _manager;
    List<Animator> _animators = new List<Animator>();
    MergeUpgrade _upgrader;
    [SerializeField] UnityEvent _event1, _event2, _event3;
    public static int MAX_COUNT;
    int _npcCount;
    private void Awake()
    {
        _manager = FindObjectOfType<UpgradeManager>();
        _upgrader = FindObjectOfType<MergeUpgrade>();
    }
    private void OnEnable()
    {
        _manager.NPCCountChangeHandler += OnNPCChange;
    }

    private void Start()
    {
        MAX_COUNT = _spawnPoints.Length;
        Load();
    }
    private void OnDisable()
    {
        _manager.NPCCountChangeHandler += OnNPCChange;

    }
    public FriendlyNPC Spawn(int tier, int nPCCount)
    {
        FriendlyNPC npc = GameObject.Instantiate(_prefabs[tier],
                                                 _spawnPoints[nPCCount].transform.position,
                                                 _spawnPoints[nPCCount].rotation).
                                                GetComponent<FriendlyNPC>();
        npc.FollowTransform = _spawnPoints[nPCCount];
        return npc;
    }
    public void Spawn(int tier, Animator animator)
    {
        _animators.Add(animator);
        if (_animators.Count == 3)
        {
            StartCoroutine(MergeCoroutine(tier));
        }
    }

    IEnumerator MergeCoroutine(int tier)
    {
        _animators.ForEach(x =>
        {
            FriendlyNPC npc = x.GetComponentInParent<FriendlyNPC>();
            _manager.RemoveFromList(npc);
            Destroy(npc.gameObject, 0.1f);
        });
        _animators = new List<Animator>();

        FriendlyNPC npc = GameObject.Instantiate(_prefabs[tier],
                                                _spawnPoints[_manager.FriendlyNPCCount].position,
                                                _spawnPoints[_manager.FriendlyNPCCount].rotation,
                                                _spawnPoints[_manager.FriendlyNPCCount]
                                                ).GetComponent<FriendlyNPC>();

        npc.FollowTransform = _spawnPoints[_manager.FriendlyNPCCount];
        _manager.AddToList(npc);
        npc.GetComponentInChildren<Animator>().Play("RUN");

        _event3.Invoke();
        yield return new WaitForSeconds(0.5f);
        _upgrader.OnMergeFinished();
    }
    void Load()
    {
        string scene = SceneManager.GetActiveScene().buildIndex.ToString();

        if (PlayerPrefs.HasKey(scene + "LEVEL0"))
        {
            for (int i = 0; i < 5; i++)
            {
                if (!PlayerPrefs.HasKey(scene + "LEVEL" + i.ToString())) continue;
                int count = PlayerPrefs.GetInt(scene + "LEVEL" + i.ToString());

                for (int j = 0; j < count; j++)
                {
                    Spawn(i, _manager.FriendlyNPCCount);
                    _manager.FriendlyNPCCount++;
                }
            }
        }

        _manager.InitListAfterLoad();
    }
    private void OnNPCChange(List<FriendlyNPC> lst)
    {
        string scene = SceneManager.GetActiveScene().buildIndex.ToString();
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt(scene + "LEVEL" + i.ToString(), 0);
        }

        foreach (FriendlyNPC npc in lst)
        {
            int count = PlayerPrefs.GetInt(scene + "LEVEL" + npc.Tier.ToString());
            count += 1;
            PlayerPrefs.SetInt(scene + "LEVEL" + npc.Tier.ToString(), count);
        }
    }
}