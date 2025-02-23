using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    public float force = 5f;
    private float directionX = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Face the correct direction
        if (transform.rotation == Quaternion.identity)
        {
            directionX = -1f;
        }
        transform.localScale = new Vector3(transform.localScale.x * directionX, transform.localScale.y, transform.localScale.z);

        // Add force
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(directionX,0) * force);

        // wait 4 seconds before destroying bullet
        Invoke("Die", 4f);
    }
    
    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            //collision.gameObject.GetComponent<PlayerController>().Die();
            Die();
        }
    }
}
