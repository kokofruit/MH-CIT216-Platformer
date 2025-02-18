using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField] GameObject boneDrop;
    int direction = 1;

    #region Movement
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.85f);
        Debug.DrawRay(transform.position, new Vector3(0, -0.85f), Color.red, 0.5f);

        if (hit.collider == null)
        {
            direction *= -1;
            Flip();
        }

        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x - 1 * direction, transform.position.y), Time.deltaTime);

    }
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    #endregion

    // Death
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            Instantiate(boneDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
