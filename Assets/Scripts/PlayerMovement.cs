using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimentação")]
    public float speed = 6f;
    public float rotationSpeed = 200f;
    public float jumpHeight = 1.5f;

    [Header("Gravidade")]
    public float gravity = -9.81f;
    private Vector3 velocity;

    [Header("Verificação de Chão")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    [Header("Dead Zone")]
    public float deadZone = 0.1f;

    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck não atribuído no Inspector!");
        }
    }

    void Update()
    {
        // Verifica se está no chão
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }

        // Reset da velocidade vertical ao tocar o chão
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Inputs brutos
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Zera animações
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsBack", false);

        // Movimento para frente e para trás
        Vector3 move = Vector3.zero;

        if (vertical > deadZone)
        {
            move += transform.forward;
            animator.SetBool("IsWalking", true);
        }
        else if (vertical < -deadZone)
        {
            move -= transform.forward;
            animator.SetBool("IsBack", true);
        }

        // Aplica movimentação
        controller.Move(move.normalized * speed * Time.deltaTime);

        // Gira com A e D
        if (Mathf.Abs(horizontal) > deadZone)
        {
            transform.Rotate(Vector3.up * horizontal * rotationSpeed * Time.deltaTime);
        }

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}
