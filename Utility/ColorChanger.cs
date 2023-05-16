using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorChanger : MonoBehaviour
{

    public Material defaultMaterial;
    public Color newColor;
    public float timeScale;
  
    public void ColorChange()
    {
        defaultMaterial.DOColor(newColor,timeScale);
    }
}
