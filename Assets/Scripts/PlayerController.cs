using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Serialized
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float maxSpeed;
    [SerializeField] float gravityMultiplier;
    [SerializeField] GameObject fire;
    [SerializeField] Transform firePoint;

    // Private
    Vector2 movementVector;
    Animator animator;                                                                      // for animation
    SpriteRenderer spriteRenderer;                                                          // for sprite flip
    Rigidbody2D rb;                                                                         // for physics
    bool isGrounded = false;
    bool jump = false;
    float fireRate = 0.3f;
    float nextFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();                                                // for animation
        spriteRenderer = GetComponent<SpriteRenderer>();                                    // for sprite flip
        rb = GetComponent<Rigidbody2D>();                                                   // for physics
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("player_speed", Mathf.Abs(movementVector.x));                     // for animation

        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            if (movementVector.x > 0)
            {
                //transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                rb.AddForce(Vector2.right * movementSpeed);
                spriteRenderer.flipX = false;
            }
            else if (movementVector.x < 0)
            {
                //transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
                rb.AddForce(Vector2.left * movementSpeed);
                spriteRenderer.flipX = true;
            }
        }

        if (jump)
        {
            //transform.Translate(Vector2.up * jumpHeight * Time.deltaTime);
            //StartCoroutine("LerpJump");
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;
        }

        if (rb.velocity.y < 0) // falling
        {
            rb.gravityScale = gravityMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        //Debug.Log(movementVector.x);
    }

    #region JUMPING
    public void OnJump(InputValue movementValue)
    {
        //transform.Translate(Vector2.up * jumpHeight * Time.deltaTime);
        //Debug.Log("Jumping!! :P");
        if (isGrounded)
        {
            jump = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            //Debug.Log("touching ground");
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            //Debug.Log("NOT touching ground");
        }
    }

    IEnumerator LerpJump()
    {
        float desired = transform.position.y + 3f;

        while (transform.position.y < desired)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + jumpHeight * Time.deltaTime);
            yield return new WaitForSeconds(0.05f);
        }
    }
    #endregion

    public void OnFire(InputValue fireValue)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetTrigger("is_shooting");
            Instantiate(fire, position: firePoint.position, rotation: Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            GameManager.instance.DecreaseLives();
            
            SceneManager.LoadScene(0);
        }
    }
}
