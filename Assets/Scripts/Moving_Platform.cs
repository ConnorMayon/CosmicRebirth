using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    public float moveDistance = 10f; // Distance the platform moves left and right
    public float moveSpeed = 4f; // Speed at which the platform moves

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + new Vector3(moveDistance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            // Move towards the start position
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingLeft = false;
            }
        }
        else
        {
            // Move towards the end position
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                movingLeft = true;
            }
        }
    }
}
