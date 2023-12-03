using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Themes : MonoBehaviour
{

    private static int nextTheme = 0;
    private static int currentTheme = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public static void updateCurrentTheme()
    {
        currentTheme = nextTheme;
    }

    public static void switchTheme()
    {
        //nextTheme = Random.Range(0, 4);
        nextTheme = 2;
    }

}
