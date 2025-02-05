using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    int direction = 1;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
        Debug.DrawRay(transform.position, new Vector3(0, -0.7f), Color.red, 0.5f);

        if (hit.collider == null)
        {
            direction *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);
    }
}
