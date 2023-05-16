using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public bool IsEngaging = false;
    public float BaseSpeed;
    public float Speed;

    private void Update()
    {
        if (IsEngaging) return;
        transform.position += (transform.forward * Speed * Time.deltaTime);
    }
    private void Start()
    {
        Speed = BaseSpeed;
    }
}
