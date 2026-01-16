using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 2f;
    public float gravity = -9.81f;

    [Header("Air Control")]
    [Tooltip("Fraction of ground speed allowed while airborne (0 = no control, 1 = full control)")]
    public float airControl = 0.6f;
    [Tooltip("How quickly horizontal velocity moves toward input while airborne (higher = more responsive)")]
    public float airAcceleration = 6f;

    private CharacterController controller;
    private Vector3 velocity;

    // preserved horizontal velocity to keep momentum when leaving ground
    private Vector3 horizontalVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        horizontalVelocity = Vector3.zero;
    }

    private void Update()
    {
        //horizontal input
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        //ensure a small downward force when grounded
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //jump input
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        //apply gravity
        velocity.y += gravity * Time.deltaTime;

        //apply final movement
        Vector3 finalMove = move * speed + new Vector3(0, velocity.y, 0);
        controller.Move(finalMove * Time.deltaTime);
    }
}
