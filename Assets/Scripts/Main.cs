using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // function to start the game
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // function to that goes to the tip scene
    public void GoToTips()
    {
        SceneManager.LoadScene("TipsScene");
    
    
    }

    // function to that goes to the start scene
    public void LoadStart()
    {
        SceneManager.LoadScene("StartScene");
    }

}
