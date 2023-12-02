using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    static private CameraFollow S;

    [Header("inscribed")]
    public GameObject player;

    [Header("Dyanmic")]
    public float camWidth;
    public float camHeight;
    Vector3 pos;

    public float camZ;

    private void Awake()
    {
        S = this;
        camHeight = Camera.main.orthographicSize;
        camWidth = Camera.main.aspect * camHeight;
        pos.y = transform.position.y;
        pos.z = transform.position.z;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > transform.position.x - camWidth / 2) 
        {
            pos.x = player.transform.position.x + camWidth / 2;
            pos.y = player.transform.position.y + camHeight / 20;
            transform.position = pos;
        }
    }
}
