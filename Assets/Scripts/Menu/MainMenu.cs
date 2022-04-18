using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Metroid Zone");
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
