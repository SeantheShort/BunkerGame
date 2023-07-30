using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    float xInput;
    float zInput;
    public float moveSpeed = 5f;
    public float gravityScale = -20f;
    public bool isGrounded;
    public Vector3 velocity;
    public float jumpForce = 10f;
    public float jumpTimer = 1f;

    // Object References
    public CharacterController characterController;
    public Transform groundCheck;
    public LayerMask groundMask;


    void Update()
    {
        // Movement Variable Update
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        // Moving Character
        Vector3 movePos = transform.right * xInput + transform.forward * zInput;
        characterController.Move(movePos * moveSpeed * Time.deltaTime);

        // Gravity Stuff
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, groundMask); //Checks to see if character is grounded
        if (isGrounded && velocity.y < 0) { velocity.y = -2f; } // Allows for velocity & gravity if player is not grounded
        velocity.y += gravityScale * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetButton("Jump") && jumpTimer <= 2f)
        {
            jumpTimer += Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            velocity.y += jumpForce * jumpTimer;
            Debug.Log(jumpForce * jumpTimer);
            jumpTimer = 1f;
        }

    }
}
