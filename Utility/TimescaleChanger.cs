using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimescaleChanger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            float timescale = Time.timeScale;
            Time.timeScale += 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            float timescale = Time.timeScale;
            Time.timeScale -= 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
        }
    }
}
