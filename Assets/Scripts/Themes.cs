using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Themes : MonoBehaviour
{

    private static int themeChoice = 0;

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
        return themeChoice;
    }

    public static void switchTheme()
    {
        themeChoice = Random.Range(0, 3);
    }
}
