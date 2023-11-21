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
        // Only trigger when jumped on - uses offset of 2
        if (collision.gameObject.CompareTag("Enemy") && (transform.position.y > (collision.transform.position.y + 3.5f)))
        {
            Debug.Log("Collision detected! Yes");

            Destroy(collision.gameObject);
        }

    }
}
