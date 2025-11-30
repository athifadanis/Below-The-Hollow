using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Camera Settings")]
    public Camera playerCamera;
    public float cameraFollowSpeed = 3f;
    public Vector3 cameraOffset = new Vector3(5, 2, 0); // Side-view offset

    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void Update()
    {
        // Ground check
        isGrounded = characterController.isGrounded;

        // Reset velocity ketika di tanah
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Input movement - DIPERBAIKI
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movement direction relative to character forward
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;

        // Apply movement menggunakan CharacterController - DIPERBAIKI
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Animator - DIPERBAIKI
        bool isMoving = (horizontal != 0f || vertical != 0f) && isGrounded;
        animator.SetBool("isWalking", isMoving);
        animator.SetBool("isGrounded", isGrounded);

        // Jump handling - DIPERBAIKI (pakai CharacterController, bukan Rigidbody)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void LateUpdate()
    {
        UpdateCamera();
    }

    void UpdateCamera()
    {
        if (playerCamera == null) return;

        Vector3 targetPosition = transform.position + cameraOffset;
        Vector3 currentCamPos = playerCamera.transform.position;

        // Side-view camera: hanya follow di sumbu X dan Y
        Vector3 newCamPos = new Vector3(targetPosition.x, targetPosition.y, currentCamPos.z);

        playerCamera.transform.position = Vector3.Lerp(
            currentCamPos,
            newCamPos,
            cameraFollowSpeed * Time.deltaTime
        );

        playerCamera.transform.LookAt(transform.position + Vector3.up * 1f);
    }
}