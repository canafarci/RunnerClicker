using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject FirstCamera;
    public GameObject SecondCamera;
    private void Update()
    {
        if (Input.anyKey)
        {
            StartGame();
        }

    }

    private void StartGame()
    {
        foreach (MobNPC npc in FindObjectsOfType<MobNPC>())
        {
            npc.enabled = true;
        }
        FindObjectOfType<MoveForward>().enabled = true;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("NPC"))
        {
            go.GetComponentInChildren<Animator>().Play("RUN");
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("FriendlyNPC"))
        {
            go.GetComponentInChildren<Animator>().Play("RUN");
        }

        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().Play("RUN");

        CameraController.Instance.ActivateCamera("FirstCamera");

        this.enabled = false;
    }
}
