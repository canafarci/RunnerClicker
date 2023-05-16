using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootSwitch : MonoBehaviour
{

    public void StartGame()
    {
        if (PlayerPrefs.HasKey("currentScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("currentScene"));
        }
        else
        {
            PlayerPrefs.SetInt("currentScene", 1);
            SceneManager.LoadScene(1);
        }
    }
}
