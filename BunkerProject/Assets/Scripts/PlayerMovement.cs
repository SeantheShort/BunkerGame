using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    float xInput;
    float zInput;
    public float moveSpeed;
    public float gravityScale;
    public bool isGrounded;
    public Vector3 velocity;
    public float jumpForce;
    public float jumpTimer;
    public bool hasGlider;
    public bool isGliding;
    public bool isSprinting;
    public bool isClimbing;
    public bool climbable;
    public float climbForce;
    public float FOVLerpTime;

    // Object References
    public CharacterController characterController;
    public Camera mainCamera;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform climbCheck;
    public LayerMask climbMask;

    void Start() // Setting values of variables
    {
        moveSpeed = 5f;
        gravityScale = -20f;
        jumpForce = 10f;
        jumpTimer = 1f;
        hasGlider = true; //CHANGE THIS WHEN ACQUIRED
        climbable = false;
        climbForce = 3f;
        FOVLerpTime = 0f;
    }

    void Update()
    {
        // Movement Variable Update
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        // Moving Character
        Vector3 movePos = transform.right * xInput + transform.forward * zInput;
        characterController.Move(movePos * moveSpeed * Time.deltaTime);

        // Wall Climbing
        climbable = Physics.CheckSphere(climbCheck.position, 0.6f, climbMask);
        if (climbable && zInput > 0f)
        {
            isClimbing = true;
            velocity.y = 0f;
            characterController.Move(new Vector3(0f, climbForce * Time.deltaTime, 0f));
        }
        else { isClimbing = false; }

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

        // Adaptive FOV (70 to 75)
        if (isGliding || isSprinting)
        {
            mainCamera.fieldOfView = Mathf.Lerp(70, 75, FOVLerpTime);
            FOVLerpTime += 10f * Time.deltaTime;
            FOVLerpTime = Mathf.Min(1, FOVLerpTime);
        }
        else
        { 
            mainCamera.fieldOfView = 70;
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