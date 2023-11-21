using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    public float enemySpeed = 1f;
    public int enemySwitch = 3000;
    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x -= enemySpeed * Time.deltaTime;

        if(counter == enemySwitch)
        {
            enemySpeed *= -1f;
            counter = 0;
        }

        counter++;

        transform.position = pos;

    }
}
