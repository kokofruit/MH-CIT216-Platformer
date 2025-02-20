using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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
    AudioSource audioSource;
    bool isGrounded = false;
    bool jump = false;
    float fireRate = 0.3f;
    float nextFire = 0f;
    bool facingRight = true;

    [Header("Audio Clips")]
    public AudioClip shootSound;
    public AudioClip jumpSound;
    public AudioClip collectSound;
    public AudioClip hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();                                                // for animation
        animator.SetBool("player_hurt", false);
        spriteRenderer = GetComponent<SpriteRenderer>();                                    // for sprite flip
        rb = GetComponent<Rigidbody2D>();                                                   // for physics
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("player_speed", Mathf.Abs(movementVector.x));                     // for animation
        if (movementVector.x > 0 && !facingRight)
        {
            //transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
                
            //spriteRenderer.flipX = false;
            Flip();
            facingRight = true;
        }
        else if (movementVector.x < 0 && facingRight)
        {
            //transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
            //spriteRenderer.flipX = true;
            Flip();
            facingRight = false;
            
        }

        if (jump)
        {
            //transform.Translate(Vector2.up * jumpHeight * Time.deltaTime);
            //StartCoroutine("LerpJump");
            rb.AddForce(Vector2.up * jumpHeight);
            SoundManager.instance.PlaySound(jumpSound, volume:0.75f);
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

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) < maxSpeed && movementVector.x != 0) rb.AddForce(GetDirection() * movementSpeed);
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

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
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
        GameManager.instance.WinSequence();
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            animator.SetTrigger("is_shooting");
            //audioSource.PlayOneShot(audioSource.clip, Random.Range(0.8f, 1));
            SoundManager.instance.PlaySound(shootSound, Random.Range(0.9f, 1.1f), Random.Range(0.8f, 1));
            Instantiate(fire, firePoint.position, facingRight ? firePoint.rotation : Quaternion.Euler(0, 180, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary")) Die();
        else if (collision.gameObject.CompareTag("PickUp"))
        {
            GameManager.instance.IncreaseBones();
            SoundManager.instance.PlaySound(collectSound);
            Destroy(collision.gameObject);
        }
    }

    private void DeathReset()
    {
        GameManager.instance.DecreaseLives();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Die(){
        print("ouch!");
        GetComponent<PlayerInput>().DeactivateInput();
        animator.SetBool("player_hurt", true);
        rb.gravityScale = 0;
        SoundManager.instance.PlaySound(hurtSound);
        Invoke("DeathReset", 0.5f);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public Vector2 GetDirection()
    {
        //return facingRight;
        if (facingRight) return Vector2.right;
        else return Vector2.left;
    }

}
