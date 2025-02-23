using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float raycastheight;
    
    bool raging = false;
    
    int direction = 1;
    Vector2 origPos;
    [SerializeField] float movementRange;

    [SerializeField] AudioClip deathSound;

    Animator anim;
    GameObject player;

    private void Start()
    {
        origPos = transform.position;
        anim = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    void FixedUpdate()
    {
        if (raging)
        {
            // If in attack mode, move towards the player
            transform.position = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime);
            // Stay facing the player
            if (Mathf.Sign(transform.position.x - player.transform.position.x) != direction)
            {
                Flip();
            }
        }
        else
        {
            // Raycast down and check for the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastheight);
            Debug.DrawRay(transform.position, new Vector3(0, -1 * raycastheight), Color.red, 0.5f);

            // If the player is found, enter attack mode
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                raging = true;
                anim.SetBool("raging", true);
                return;
            }

            // Move ghost forwards
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);

            // Flip if too far from starting position
            if (Mathf.Abs(origPos.x - transform.position.x) >= movementRange)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        direction *= -1;
    }

    // Death
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            SoundManager.instance.PlaySound(deathSound);
            Destroy(gameObject);
        }
    }
}
