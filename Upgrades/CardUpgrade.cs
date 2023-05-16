using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardUpgrade : MonoBehaviour
{
    protected CardUpgradeCanvas _canvas;
    protected virtual void Awake()
    {
        _canvas = GetComponentInParent<CardUpgradeCanvas>();
    }

    public virtual void OnUpgradeClicked()
    {
        UpgradeClicked();
    }

    protected void UpgradeClicked()
    {
        _canvas.DisableCanvas();
    }
}
