using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsPanel;
    public void StartGame(){
        SceneManager.LoadScene("DebugLevel");
    }
    public void Instructions(){
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }else{
            Debug.LogWarning("nao abriu");
        }

    }
    public void ExitGame(){
        Application.Quit();
    }
}
