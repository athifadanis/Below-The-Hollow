using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController _controller;
    private Vector3 velocity;
    private bool isGrounded;

    // --- TAMBAHAN 1: Variabel Animator ---
    private Animator animator;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        // --- TAMBAHAN 2: Cari komponen Animator saat game mulai ---
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Cek Ground
        // (Pastikan groundCheck tidak error/null)
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. Input Gerak
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 right = transform.right;
        Vector3 forward = transform.forward;
        right.y = 0f;
        forward.y = 0f;
        right.Normalize();
        forward.Normalize();

        Vector3 move = right * x + forward * z;

        _controller.Move(move * moveSpeed * Time.deltaTime);

        // --- TAMBAHAN 3: LOGIKA ANIMASI (PENTING) ---
        // Jika x atau z lebih dari 0.1, berarti player sedang bergerak
        bool isMoving = (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f);

        // Kirim sinyal ke Animator
        // "isWalking" harus SAMA PERSIS huruf besar/kecilnya dengan di Animator
        if (animator != null)
        {
            animator.SetBool("isWalking", isMoving);
        }
        // -------------------------------------------

        // 3. Gravitasi
        velocity.y += gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }
}