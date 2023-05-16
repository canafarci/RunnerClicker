using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCardUpgrade : CardUpgrade
{
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _text;
    WeaponUpgrade _currentUpgrade;
    PlayerFighter _fighter;
    protected override void Awake()
    {
        base.Awake();
        _fighter = FindObjectOfType<PlayerFighter>();
    }
    private void OnEnable()
    {
        _currentUpgrade = References.Instance.GameConfig.Weapons[PlayerPrefs.GetInt("Weapon")];
        _image.sprite = _currentUpgrade.CardImage;
        _text.text = _currentUpgrade.WeaponText;
    }

    public override void OnUpgradeClicked()
    {
        int curLevel = PlayerPrefs.GetInt("Weapon");
        _fighter.ChangeWeapon(curLevel);
        PlayerPrefs.SetInt("Weapon", curLevel + 1);
        //disables canvas and returns to default timescalse
        base.OnUpgradeClicked();
    }

}


