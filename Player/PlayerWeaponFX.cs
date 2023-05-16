using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponFX : MonoBehaviour
{
    [SerializeField] ParticleSystem _pistolFX;
    [SerializeField] ParticleSystem[] _rifleFXs;
    [SerializeField] ParticleSystem[] _minigunFXs;

    public void PlayPistolFX()
    {
        _pistolFX.Play();
    }
    public void PlayRifleFX()
    {
        foreach (ParticleSystem fx in _rifleFXs)
        {
            fx.Play();
        }
    }
    public void PlayMinigunFX()
    {
        foreach (ParticleSystem fx in _minigunFXs)
        {
            fx.Play();
        }
    }
}
