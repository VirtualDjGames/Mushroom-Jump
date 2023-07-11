using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private float jumpForce = 35f;
    private float movementSpeed = 15f;
    public LayerMask groundLayer;
    public Transform groundCheckC;
    public Transform groundCheckR;
    public Transform groundCheckL;

    private float jumpDelay = 0.2f; // Tiempo de espera antes de permitir el siguiente salto
    private float jumpTimer; // Temporizador para contar el tiempo transcurrido desde el último salto
    private bool canJump; // Indicador de si se permite el salto

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpDelay; // Establecer el temporizador inicial al valor del tiempo de espera
        canJump = false; // No permitir el salto al inicio
    }

    void Update()
    {
        // Comprueba si el champiñón está en el suelo
        bool isGroundedC = Physics2D.OverlapCircle(groundCheckC.position, 0.1f, groundLayer);
        bool isGroundedR = Physics2D.OverlapCircle(groundCheckR.position, 0.1f, groundLayer);
        bool isGroundedL = Physics2D.OverlapCircle(groundCheckL.position, 0.1f, groundLayer);

        isGrounded = isGroundedC || isGroundedR || isGroundedL;

        // Actualizar el temporizador solo si está en el suelo
        if (isGrounded)
        {
            jumpTimer += Time.deltaTime;
        }

        // Permitir el salto si ha pasado el tiempo de espera
        if (jumpTimer >= jumpDelay)
        {
            canJump = true;
        }

        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);

        // Salto
        if (isGrounded && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTimer = 0f; // Reiniciar el temporizador
            canJump = false; // No permitir el salto hasta que pase el tiempo de espera nuevamente
        }
    }
}
