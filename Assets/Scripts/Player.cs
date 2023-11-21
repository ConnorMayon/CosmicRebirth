using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Only trigger when jumped on 
        if (collision.gameObject.CompareTag("Enemy") && (transform.position.y > (collision.transform.position.y + 3.5f)))
        {
            Debug.Log("Player killed enemy");

            Destroy(collision.gameObject);
        }

        else if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player was hit by enemy");

            Vector3 pos = transform.position;
            pos.x -= 3f;
            transform.position = pos;
        }

    }
}
