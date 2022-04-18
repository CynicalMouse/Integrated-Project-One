using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;

    public GameObject options;

    public GameObject main;


    public void Start()
    {
        options = GameObject.Find("Options Menu");
        main = GameObject.Find("Main Menu");
    }

    public void ChangeToScene()
    {
        SceneManager.LoadScene("Metroid Zone");
        Debug.Log("pressed");
    }
    
    public void OptionsScene()
    {
        options.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
    }

    public void GameQuit()
    {
        // pnly works in editor DONT use in final build
        //UnityEditor.EditorApplication.isPlaying = false;

        //Doesnt Work in editor (USE IN FINAL BUILD)
        Application.Quit(); 
        Debug.Log("pressed");
    }
}
