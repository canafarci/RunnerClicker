using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardUpgradeCanvas : MonoBehaviour
{
    [SerializeField] GameObject _canvas;

    public void EnableCanvas()
    {
        Time.timeScale = 0.001f;
        _canvas.SetActive(true);
    }

    public void DisableCanvas()
    {
        Time.timeScale = 1f;
        _canvas.SetActive(false);
    }
}
