using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedCardUpgrade : CardUpgrade
{
    public override void OnUpgradeClicked()
    {
        FindObjectOfType<ClickHandler>().IncreaseAttackSpeedMultiplier();
        //disables canvas and returns to default timescalse
        base.OnUpgradeClicked();
    }
}
