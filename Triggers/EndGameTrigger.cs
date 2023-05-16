using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    string levelname;
    private void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelname = "level" + sceneIndex.ToString();
        TinySauce.OnGameStarted(levelname);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<MoveForward>().enabled = false;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int loadIndex = sceneIndex == 1 ? 2 : 1;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("currentScene", sceneIndex == 1 ? 2 : 1);
        TinySauce.OnGameFinished(true, Resource.Instance.Money, levelname);
        SceneLoader.Instance.LoadScene(loadIndex);
    }
    private void OnApplicationQuit()
    {
        TinySauce.OnGameFinished(false, Resource.Instance.Money, levelname);
    }
}
