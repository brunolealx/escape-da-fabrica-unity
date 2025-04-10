using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;               // Velocidade de movimento
    public float jumpHeight = 1.5f;        // Altura do pulo
    public float gravity = -9.81f;         // Gravidade personalizada
    public Transform groundCheck;          // Objeto vazio no pé do player
    public float groundDistance = 0.4f;    // Raio da esfera para checar o chão
    public LayerMask groundMask;           // Qual layer é considerado chão?

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Verifica se o groundCheck foi atribuído
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck não atribuído no inspector!");
        }
    }


    void Update()
    {
        // Garante que o groundCheck não esteja nulo antes de usar
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        else
        {
            isGrounded = false;
        }

        // Se está no chão e com velocidade negativa, reseta para evitar queda acumulada
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movimento WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplica gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Desenha a esfera de detecção do chão no editor (debug visual)
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}
