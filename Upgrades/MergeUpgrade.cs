using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MergeUpgrade : Upgrade
{
    List<FriendlyNPC> _npcsToMerge = new List<FriendlyNPC>();
    List<FriendlyNPC> _currentList = new List<FriendlyNPC>();
    bool _isMerging = false;
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
    override public void OnUpgradeClicked()
    {
        if (_insufficientCount || _isMerging) return;
        _isMerging = true;
        base.OnUpgradeClicked();
        DisableButton();
        _text.SetMerging();
    }
    public void OnMergeFinished()
    {
        _isMerging = false;
        _text.SetCost(_moneyToUpgrade);
        _button.interactable = true;
    }
    protected override void SetLevelValues(int level)
    {
        GameConfig config = References.Instance.GameConfig;
        _moneyToUpgrade = config.MergeUpgrades[_level - 1].Cost;
        if (_hasLoaded)
        {
            _manager.OnMergeBuilders(_npcsToMerge);
        }
        else
            _hasLoaded = true;

        _npcsToMerge = new List<FriendlyNPC>();
        base.SetLevelValues(level);
    }
    void CheckCount(List<FriendlyNPC> friendlyNPCList)
    {
        _currentList = friendlyNPCList;
        if (friendlyNPCList.Count < 3)
        {
            DisableTextWithMax();
            _insufficientCount = true;
            return;
        }
        int maxLevel = friendlyNPCList.Max(x => x.Tier);

        List<List<FriendlyNPC>> masterList = new List<List<FriendlyNPC>>();

        for (int i = 0; i < maxLevel + 1; i++)
            masterList.Add(friendlyNPCList.Where(x => x.Tier == i).ToList());

        foreach (List<FriendlyNPC> lst in masterList)
        {
            if (lst.Count > 2)
            {
                _npcsToMerge = lst;
                _insufficientCount = false;
                if (CheckCanClick(Resource.Instance.Money))
                    EnableButton();
                else
                {
                    DisableButton();
                    _text.SetCost(_moneyToUpgrade);
                }
                return;
            }
        }
        _insufficientCount = true;
        DisableTextWithMax();
    }
    protected override bool CheckCanClick(float currentMoney)
    {
        if (_insufficientCount) return false;
        else
        {
            return base.CheckCanClick(currentMoney);
        }
    }
}
