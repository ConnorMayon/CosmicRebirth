using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    bool isPlatform;
    float horMovMult = 30f;
    float verMovMult = 8f;

    // Start is called before the first frame update
    void Start()
    {
        // get player component
        rb = GetComponent<Rigidbody>();

        // freeze the rotation on the player
        rb.freezeRotation = true;

        // set platform tag to true
        isPlatform = true;
    } 

    // Update is called once per frame
    void Update()
    {
        // get the x axis input
        float dirX = Input.GetAxisRaw("Horizontal");

        // left and right movement, keeps same y axis value
        rb.velocity = new Vector2(dirX * horMovMult, rb.velocity.y);

        // check if input for jump is activated and player is on a platform
        if (Input.GetButtonDown("Jump") && isPlatform)
        {
            // set y axis value and keep same x axis value
            rb.velocity = new Vector2(rb.velocity.x, verMovMult);

            // set platform flag to false
            isPlatform = false;
        }
    }

    // called when player collides with a platform
    private void OnCollisionEnter(Collision collision)
    {
        // check if collision is with a platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            // set platform flag to true
            isPlatform = true;
        }
    }
}
