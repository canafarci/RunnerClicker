using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelProgressSlider : MonoBehaviour
{
    [SerializeField] Slider _progressSlider;
    [SerializeField] TextMeshProUGUI _percentageText, _levelText;
    void Start()
    {
        _progressSlider.maxValue = 1;
        _progressSlider.minValue = 0;
    }

    public void SetValues(int exp, int expRequired, int level)
    {
        _levelText.text = "LEVEL " + level.ToString();
        float val = (float)exp / (float)expRequired;
        _progressSlider.value = val;
        _percentageText.text = "%" + (val * 100f).ToString("F0");
    }


}
