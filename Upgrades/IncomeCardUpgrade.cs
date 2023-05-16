using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeCardUpgrade : CardUpgrade
{
    public override void OnUpgradeClicked()
    {
        Resource.Instance.IncreaseIncomeMultiplier();
        //disables canvas and returns to default timescalse
        base.OnUpgradeClicked();
    }
}
