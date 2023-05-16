using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float Money { get { return _currentMoney; } }
    public event Func<float, bool> MoneyChangeHandler;
    float _currentMoney;
    float _multiplier;
    public static Resource Instance;
    void Awake()
    {
        InitSingleton();
        Init();
    }
    private void InitSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Init()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", References.Instance.GameConfig.StartMoney);
            PlayerPrefs.SetFloat("IncomeMultiplier", 1f);
        }

        _currentMoney = PlayerPrefs.GetInt("Money");
        _multiplier = PlayerPrefs.GetFloat("IncomeMultiplier");
    }

    public void ZeroMoney()
    {
        PlayerPrefs.SetInt("Money", 0);
        _currentMoney = 0;
        MoneyChangeHandler?.Invoke(_currentMoney);
    }
    public void OnMoneyChange(float change)
    {
        _currentMoney += change * _multiplier;
        PlayerPrefs.SetInt("Money", (int)_currentMoney);
        MoneyChangeHandler?.Invoke(_currentMoney);
    }
    public void IncreaseIncomeMultiplier()
    {
        _multiplier *= 1.2f;
        PlayerPrefs.SetFloat("IncomeMultiplier", _multiplier);
    }
}
