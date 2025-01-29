using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Serialized
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpHeight;

    // Private
    Vector2 movementVector;
    Animator animator;                                                                      // for animation
    SpriteRenderer spriteRenderer;                                                          // for sprite flip

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();                                                // for animation
        spriteRenderer = GetComponent<SpriteRenderer>();                                    // for sprite flip
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("player_speed", Mathf.Abs(movementVector.x));                     // for animation
        if (movementVector.x > 0) transform.Translate(Vector2.right * movementSpeed * Time.deltaTime); // vector is
        else if (movementVector.x < 0) transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);

        if (movementVector.x < 0) spriteRenderer.flipX = true;
        else if (movementVector.x > 0) spriteRenderer.flipX = false;
    }

    public void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector.x);
    }

    public void OnJump(InputValue movementValue)
    {
        transform.Translate(Vector2.up * jumpHeight * Time.deltaTime);
        Debug.Log("Jumping!! :P");
    }
}
