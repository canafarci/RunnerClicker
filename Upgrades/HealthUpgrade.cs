using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Upgrade
{
    [SerializeField] ParticleSystem _healthUPfx;
    override public void OnUpgradeClicked()
    {
        int maxLevel = References.Instance.GameConfig.HealthUpgrades.Length;
        if (maxLevel == _level) { return; }

        base.OnUpgradeClicked();
    }
    protected override void SetLevelValues(int level)
    {
        GameConfig config = References.Instance.GameConfig;

        _moneyToUpgrade = config.HealthUpgrades[_level - 1].Cost;
        _manager.SetMaxHealth(config.HealthUpgrades[_level - 1].Health);

        if (_hasLoaded)
            _healthUPfx.Play();
        else
            _hasLoaded = true;

        _text.SetCost(_moneyToUpgrade);
        _manager.UpdateText();

        base.SetLevelValues(level);

        int maxLevel = References.Instance.GameConfig.HealthUpgrades.Length;
        if (maxLevel == _level) { OnMaxLevel(); }
    }
}
