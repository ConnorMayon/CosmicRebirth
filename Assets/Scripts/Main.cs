using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

}
