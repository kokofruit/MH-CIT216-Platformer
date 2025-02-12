using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float force = 5f;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        direction = player.GetComponent<PlayerController>().GetDirection();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * force);
        //rb.velocity = Vector2.right * force;

        // wait 4 seconds before destroying bullet
        Invoke("Die", 4f);
    }
    
    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
