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
    public bool hasGlider = true; //CHANGE THIS WHEN ACQUIRED
    public bool isGliding;
    public bool isSprinting;
    public float FOVLerpTime = 0f;

    // Object References
    public CharacterController characterController;
    public Camera mainCamera;
    public Transform groundCheck;
    public LayerMask groundMask;

    // Reference UI Script (so functions can be used)
    private UIManager UIManager;


    void Update()
    {
        // Movement Variable Update
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        // Moving Character
        Vector3 movePos = transform.right * xInput + transform.forward * zInput;
        characterController.Move(movePos * moveSpeed * Time.deltaTime);

        // Sprinting
        if (Input.GetKey(KeyCode.LeftControl) && !isGliding && zInput != 0f)
        {
            moveSpeed = 10f;
            isSprinting = true;
        }
        else if (!isGliding)
        {
            moveSpeed = 5f;
            isSprinting = false;
        }

        // Gravity Stuff
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, groundMask); //Checks to see if character is grounded
        if (isGrounded && velocity.y < 0) { velocity.y = -2f; } // Allows for velocity & gravity if player is not grounded
        velocity.y += gravityScale * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetButton("Jump") && jumpTimer <= 2f) //Times how long the jump button is held
        {
            jumpTimer += Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump") && isGrounded) // Applies Jump Force & Time
        {
            velocity.y += jumpForce * jumpTimer;
            jumpTimer = 1f;
        }

        //Gliding
        if(!isGrounded && Input.GetButtonDown("Jump") && hasGlider && velocity.y < 5f)
        {
            gravityScale = -2.5f;
            moveSpeed = 7.5f;
            isGliding = true;
        }
        if (isGrounded && isGliding)
        {
            isGliding = false;
            gravityScale = -20f;
        }

        // Adaptive FOV (80 to 90)
        if (isGliding || isSprinting)
        {

            mainCamera.fieldOfView = Mathf.Lerp(80, 90, FOVLerpTime);
            FOVLerpTime += 7.5f * Time.deltaTime;
            FOVLerpTime = Mathf.Min(1, FOVLerpTime);
        }
        else
        { 
            mainCamera.fieldOfView = 80;
            FOVLerpTime = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.SetInteractableText("E", other.gameObject.name); // Sends interacion info to UIManager
    }

    private void OnTriggerExit(Collider other) // Hides UI after player leaves interactable
    {
        UIManager.Instance.HideInteractableText();
    }
}
