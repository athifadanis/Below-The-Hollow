using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public bool isHidden = false;
    public bool hasMatch = false;   // Inventory sederhana

    [Header("Camera Settings")]
    public bool facingRight = true; // Penting untuk logika LookAhead kamera

    private CharacterController _controller; // Gunakan underscore untuk private

    void Start() { _controller = GetComponent<CharacterController>(); }


    // Method Move (PascalCase)
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Deteksi arah hadap untuk kamera
        if (x > 0) facingRight = true;
        else if (x < 0) facingRight = false;

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * moveSpeed * Time.deltaTime);
    }
}