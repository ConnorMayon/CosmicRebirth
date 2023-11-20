using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAuto : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;

        transform.position = pos;
    }
}
