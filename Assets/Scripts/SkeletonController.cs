using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] GameObject boneDrop;
    int direction = 1;

    public AudioClip deathSound;

    #region Movement
    void FixedUpdate()
    {
        // See if there is ground below
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.85f);
        Debug.DrawRay(transform.position, new Vector3(0, -0.85f), Color.red, 0.5f);

        // Turn around if runs into an edge
        if (hit.collider == null)
        {
            Flip();
        }

        // Move
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);
    }
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        direction *= -1;
    }
    #endregion

    // Death
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            Vector2 bonePos = new Vector2(transform.position.x, transform.position.y - 0.5f);
            Instantiate(boneDrop, bonePos, Quaternion.identity);
            SoundManager.instance.PlaySound(deathSound);
            Destroy(gameObject);
        }
    }
}
