using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Themes : MonoBehaviour
{
    public Material Mat_Standard;
    public Material Mat_Winter;
    public Material Mat_Factory;
    public Material Mat_City;

    public GameObject player;
    public GameObject snowParticleSystem;

    private static int nextTheme = 0;
    private static int currentTheme = 0;
    private static int oldTheme = 0;

    public static Themes themes;

    // Start is called before the first frame update
    void Start()
    {
        themes = this;

        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.GetChild(6).GetComponent<ParticleSystem>().Pause();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int getTheme()
    {
        return nextTheme;
    }

    public static int getCurrentTheme()
    {
        return currentTheme;
    }

    // Triggered after hitting checkpoint
    public void updateCurrentTheme()
    {
        Debug.Log("Theme updated to: " + nextTheme);

        currentTheme = nextTheme;
    }

    public static void switchTheme()
    {
        Debug.Log("Setting next theme");
        oldTheme = currentTheme;

        nextTheme = Random.Range(0, 4);

        // Stops repeat themes to create a better experience
        while (nextTheme == currentTheme)
        {
            nextTheme = Random.Range(0, 4);
        }

    }

    public void updateSky()
    {
        // Switches the skybox depending on theme
        switch (oldTheme)
        {
            case 0:
                stopSnow();
                RenderSettings.skybox = Mat_Standard;
                break;
            case 1:
                startSnow();
                RenderSettings.skybox = Mat_Winter;
                break;
            case 2:
                stopSnow();
                RenderSettings.skybox = Mat_Factory;
                break;
            case 3:
                stopSnow();
                RenderSettings.skybox = Mat_City;
                break;
        }
    }

    public void startSnow()
    {
        player.transform.GetChild(6).GetComponent<ParticleSystem>().Play();
    }

    public void stopSnow()
    {
        player.transform.GetChild(6).GetComponent<ParticleSystem>().Pause();
    }

}
