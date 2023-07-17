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

    private float minXPosition; // Posición mínima en el eje X (borde izquierdo de la pantalla)
    private float maxXPosition; // Posición máxima en el eje X (borde derecho de la pantalla)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpDelay; // Establecer el temporizador inicial al valor del tiempo de espera
        canJump = false; // No permitir el salto al inicio

        // Calcular los límites de la pantalla en el eje X
        float playerWidth = GetComponent<Collider2D>().bounds.size.x;
        minXPosition = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + (playerWidth / 2);
        maxXPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - (playerWidth / 2);
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
        float horizontalInput = 0f;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
            {
                // Tocar el lado izquierdo de la pantalla
                horizontalInput = -1f;
            }
            else
            {
                // Tocar el lado derecho de la pantalla
                horizontalInput = 1f;
            }
        }

        // Limitar la posición horizontal dentro de los límites de la pantalla
        float targetX = Mathf.Clamp(transform.position.x + (horizontalInput * movementSpeed * Time.deltaTime), minXPosition, maxXPosition);
        transform.position = new Vector3(targetX, transform.position.y, transform.position.z);

        // Salto
        if (isGrounded && canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpTimer = 0f; // Reiniciar el temporizador
            canJump = false; // No permitir el salto hasta que pase el tiempo de espera nuevamente
        }
    }
}
