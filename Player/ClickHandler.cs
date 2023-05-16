using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    MoveForward _mover;
    Animator _animator;
    InputReader _reader;
    Coroutine _timeDecayRoutine;
    float _multiplier;
    private void Awake()
    {
        _mover = GetComponent<MoveForward>();
        _animator = GetComponentInChildren<Animator>();
        _reader = FindObjectOfType<InputReader>();
    }
    private void OnEnable() => _reader.ClickHandler += OnClick;
    private void OnDisable() => _reader.ClickHandler -= OnClick;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("AttackSpeedMultiplier"))
            PlayerPrefs.SetFloat("AttackSpeedMultiplier", 1f);

        _multiplier = PlayerPrefs.GetFloat("AttackSpeedMultiplier");
    }
    public void IncreaseAttackSpeedMultiplier()
    {
        _multiplier *= 1.15f;
        PlayerPrefs.SetFloat("AttackSpeedMultiplier", _multiplier);
    }

    private void OnClick()
    {
        if (_timeDecayRoutine != null)
            StopCoroutine(_timeDecayRoutine);

        _timeDecayRoutine = StartCoroutine(TimeDecay());
    }

    IEnumerator TimeDecay()
    {
        if (_mover.Speed != _mover.BaseSpeed * 2f)
        {
            _mover.Speed = _mover.BaseSpeed * 2f;
            _animator.speed = 2f * _multiplier;
        }

        yield return new WaitForSeconds(.25f);

        _mover.Speed = _mover.BaseSpeed;
        _animator.speed = 1f * _multiplier;


        _timeDecayRoutine = null;
    }
}
