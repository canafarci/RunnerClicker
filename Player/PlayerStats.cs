using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int ExpRequiredToLevelUp { get { return _experiencePerLevel + _currentLevel; } }
    [SerializeField] int _experiencePerLevel = 10;
    string _identifierExp = "PlayerExperience";
    string _identifierLevel = "PlayerLevel";
    int _currentExperience;
    int _currentLevel;
    LevelProgressSlider _progressSlider;
    CardUpgradeCanvas _canvas;
    private void Awake()
    {
        _progressSlider = GetComponent<LevelProgressSlider>();
        _canvas = FindObjectOfType<CardUpgradeCanvas>();
    }
    private void Start()
    {
        Load();
    }
    public void GainExperience(int amount)
    {
        _currentExperience += amount;

        CheckLevelUp();
        Save();

        _progressSlider.SetValues(_currentExperience,
                 (_experiencePerLevel + _currentLevel),
                  _currentLevel);
    }

    void CheckLevelUp()
    {
        if (_currentExperience >= _experiencePerLevel + _currentLevel)
        {
            _currentExperience = 0;
            _currentLevel++;
            OnLevelUp();
        }
    }

    void OnLevelUp()
    {
        _canvas.EnableCanvas();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(_identifierExp, _currentExperience);
        PlayerPrefs.SetInt(_identifierLevel, _currentLevel);
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(_identifierExp))
        {
            _currentExperience = PlayerPrefs.GetInt(_identifierExp);
            _currentLevel = PlayerPrefs.GetInt(_identifierLevel);
        }
        else
        {
            PlayerPrefs.SetInt(_identifierExp, 0);
            PlayerPrefs.SetInt(_identifierLevel, 1);
            _currentLevel = 1;
            _currentExperience = 0;
        }
        _progressSlider.SetValues(_currentExperience,
                                 (_experiencePerLevel + _currentLevel),
                                  _currentLevel);
    }
}
