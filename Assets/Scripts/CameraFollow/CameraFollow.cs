using UnityEngine;  // Importa a biblioteca UnityEngine, que cont�m fun��es essenciais para o Unity.

public class CameraFollow : MonoBehaviour
{
    // Declara uma vari�vel p�blica para o alvo (geralmente o jogador), que ser� o objeto que a c�mera seguir�.
    public Transform target;

    // Define um vetor 3D para o deslocamento da c�mera em rela��o ao alvo.
    // Esse vetor define a posi��o da c�mera para ser um pouco acima e atr�s do alvo (exemplo: player).
    public Vector3 offset = new Vector3(0, 10, -10);

    // O m�todo LateUpdate � chamado depois do m�todo Update, garantindo que o alvo (player) j� se mova antes da c�mera.
    void LateUpdate()
    {
        // Verifica se a vari�vel target n�o � nula (ou seja, se o alvo foi atribu�do no Inspector).
        if (target != null)
        {
            // Atualiza a posi��o da c�mera somando a posi��o do alvo com o offset.
            // Isso faz com que a c�mera fique na posi��o correta, com a dist�ncia definida pelo offset.
            transform.position = target.position + offset;

            // Faz a c�mera olhar sempre para o alvo (player), mantendo-o centralizado na tela.
            transform.LookAt(target);
        }
    }
}