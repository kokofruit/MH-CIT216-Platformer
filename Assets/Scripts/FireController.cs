using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * force);
        //rb.velocity = Vector2.right * force;
        // wait 4 seconds before destroying bullet
        Invoke("Die", 4f);
    }
    
    void Die()
    {
        Destroy(gameObject);
    }
}
