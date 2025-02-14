using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    int direction = 1;
    int pausedInt = 1;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine("CheckForPlayer");
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

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction * pausedInt, transform.position.y), Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator CheckForPlayer()
    {
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left * direction, 5f);
            Debug.DrawRay(transform.position, new Vector3(-1f * direction * 5f, 0), Color.cyan, 0.5f);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                animator.SetTrigger("isSpitting");
                pausedInt = 0;
                print("spit");
                yield return new WaitForSeconds(2f);
            }
            else
            {
                pausedInt = 1;
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
