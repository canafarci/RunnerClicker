using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CountUpgrade : Upgrade
{
    Sprite _oldSprite;
    override protected void Awake()
    {
        base.Awake();
        _oldSprite = _buttonImage.sprite;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        _manager.NPCCountChangeHandler += CheckCount;
    }
    override protected void OnDisable()
    {
        base.OnDisable();
        _manager.NPCCountChangeHandler -= CheckCount;
    }

    private void CheckCount(List<FriendlyNPC> friendlyNPCList)
    {
        if (friendlyNPCList.Count >= NPCSpawner.MAX_COUNT)
        {
            DisableTextWithMax();
        }
        else
        {
            _insufficientCount = false;
            CheckCanClick(Resource.Instance.Money);
        }
    }

    override public void OnUpgradeClicked()
    {
        int maxLevel = References.Instance.GameConfig.CountUpgrades.Length;
        if (maxLevel == _level) { return; }

        base.OnUpgradeClicked();
    }

    protected override void SetLevelValues(int level)
    {
        GameConfig config = References.Instance.GameConfig;

        _moneyToUpgrade = config.CountUpgrades[_level - 1].Cost;
        _text.SetCost(_moneyToUpgrade);

        if (_hasLoaded)
            _manager.SpawnWorker(_level);
        else
            _hasLoaded = true;

        base.SetLevelValues(level);

        int maxLevel = References.Instance.GameConfig.CountUpgrades.Length;

        if (maxLevel == _level) { OnMaxLevel(); }
    }

    public void OnJointUpgraded()
    {
        _buttonImage.sprite = _oldSprite;
        _text.SetCost(_moneyToUpgrade);
        _buttonImage.DOColor(new Color(183f / 255f, 0f, 1f, 1f), 0.51f);
        _text.SetCost(_moneyToUpgrade);
    }
}
