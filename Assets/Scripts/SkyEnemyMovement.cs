using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SkyEnemyMovement : MonoBehaviour
{
    public float enemySpeed = 1f;
    public int enemySwitch = 1000;
    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.y += enemySpeed * Time.deltaTime;

        if (counter == enemySwitch)
        {
            enemySpeed *= -1f;
            counter = 0;
        }

        counter++;

        transform.position = pos;
    }
}
