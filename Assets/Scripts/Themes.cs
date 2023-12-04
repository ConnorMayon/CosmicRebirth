using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Themes : MonoBehaviour
{
    public Material Mat_Standard;
    public Material Mat_Winter;
    public Material Mat_Factory;
    public Material Mat_City;

    private static int nextTheme = 0;
    private static int currentTheme = 0;

    public static Themes themes;

    // Start is called before the first frame update
    void Start()
    {
        themes = this;
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

    public void updateCurrentTheme()
    {
        switch (nextTheme)
        {
            case 0:
                RenderSettings.skybox = Mat_Standard;
                break;
            case 1:
                RenderSettings.skybox = Mat_Winter;
                break;
            case 2:
                RenderSettings.skybox = Mat_Factory;
                break;
            case 3:
                RenderSettings.skybox = Mat_City;
                break;
        }

        Debug.Log("Theme updated to: " + nextTheme);

        currentTheme = nextTheme;
    }

    public static void switchTheme()
    {
        nextTheme = Random.Range(0, 4);
    }

}
