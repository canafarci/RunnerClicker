using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] Image _hpImage;
    public void UpdateHealthSlider(float maxHealth, float currentHealth)
    {

        // matHP.SetColor("_Color", new Color(0.2117647f, 0.9137255f, 0f, 1f)); // High HP
        // matHP.SetColor("_Color", new Color(0.9137255f, 0.2964719f, 0f, 1f)); // Med HP
        // matHP.SetColor("_Color", new Color(0.9137255f, 0.08324314f, 0f, 1f)); // Low HP
        float ratio = currentHealth / maxHealth;
        _slider.value = ratio;

        if (ratio < .33f)
        {
            _hpImage.material = References.Instance.HealthMats[0];
        }
        else if (ratio > .33f && ratio < .66f)
        {
            _hpImage.material = References.Instance.HealthMats[1];
        }
        else
        {
            _hpImage.material = References.Instance.HealthMats[2];
        }

    }
    public void DisableUI()
    {
        _slider.gameObject.SetActive(false);
    }
}
