using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFX : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _hitFXs;

    public void PlayHitFX()
    {
        int randint = Random.Range(0, _hitFXs.Length);
        _hitFXs[randint].Play();
    }
}
