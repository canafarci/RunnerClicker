using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : MonoBehaviour
{
    public Health CurrentTarget { get { return _currentTarget; } }
    [SerializeField] float _damage;
    [SerializeField] Transform _weaponHolder;
    Health _currentTarget = null;
    Animator _animator;
    PlayerTrigger _trigger;
    MoveForward _mover;
    UpgradeManager _manager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _trigger = GetComponentInParent<PlayerTrigger>();
        _mover = GetComponentInParent<MoveForward>();
        _manager = FindObjectOfType<UpgradeManager>();
    }
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("Weapon"))
        {
            PlayerPrefs.SetInt("Weapon", 0);
        }
        else if (PlayerPrefs.GetInt("Weapon") >= 1)
        {
            ChangeWeapon(PlayerPrefs.GetInt("Weapon") - 1);
        }
    }
    private void Start()
    {
        StartCoroutine(AttackLoop());
    }
    private void Update()
    {
        if (_currentTarget == null)
        {
            Vector3 pos = transform.parent.position + transform.parent.position + transform.parent.forward;
            pos.y = transform.position.y;
            transform.LookAt(pos);
        }
        else
        {
            transform.LookAt(_currentTarget.transform);
        }
    }
    public void ChangeWeapon(int index)
    {
        for (int i = 0; i < _weaponHolder.childCount; i++)
            _weaponHolder.GetChild(i).gameObject.SetActive(false);

        WeaponUpgrade upgrade = References.Instance.GameConfig.Weapons[index];

        _weaponHolder.GetChild(index + 1).gameObject.SetActive(true);
        _animator.runtimeAnimatorController = upgrade.Controller;
        GetComponentInParent<CapsuleCollider>().radius = upgrade.Range;
        _damage = upgrade.Damage;
    }
    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            CheckCanAttack();
        }
    }
    public void Attack(Health target)
    {
        _currentTarget = target;
        _animator.SetBool("Attack", true);

        foreach (FriendlyNPC npc in _manager.NPCS)
        {
            npc.MoveAndAttack(target);
        }
    }
    public void Damage()
    {
        if (_currentTarget != null && !_currentTarget.IsDead)
            _currentTarget.TakeDamage(_damage, () => OnEnemyDie());
        else
        {
            CheckCanAttack();
        }
    }
    public void CheckCanAttack()
    {
        Queue<Health> q = _trigger.Enemies;
        if (_currentTarget != null && !_currentTarget.IsDead) return;
        else if (q.TryDequeue(out _currentTarget))
        {
            Attack(_currentTarget);
        }
    }

    void OnEnemyDie()
    {
        if (_trigger.Enemies.Count == 0)
        {
            _animator.SetBool("Attack", false);
            _mover.IsEngaging = false;
            foreach (FriendlyNPC npc in _manager.NPCS)
            {
                npc.MoveBack();
            }
        }
    }
}
