using UnityEngine;  // Importa a biblioteca UnityEngine, que contém funções essenciais para o Unity.

public class CameraFollow : MonoBehaviour
{
    // Declara uma variável pública para o alvo (geralmente o jogador), que será o objeto que a câmera seguirá.
    public Transform target;

    // Define um vetor 3D para o deslocamento da câmera em relação ao alvo.
    // Esse vetor define a posição da câmera para ser um pouco acima e atrás do alvo (exemplo: player).
    public Vector3 offset = new Vector3(0, 10, -10);

    // O método LateUpdate é chamado depois do método Update, garantindo que o alvo (player) já se mova antes da câmera.
    void LateUpdate()
    {
        // Verifica se a variável target não é nula (ou seja, se o alvo foi atribuído no Inspector).
        if (target != null)
        {
            // Atualiza a posição da câmera somando a posição do alvo com o offset.
            // Isso faz com que a câmera fique na posição correta, com a distância definida pelo offset.
            transform.position = target.position + offset;

            // Faz a câmera olhar sempre para o alvo (player), mantendo-o centralizado na tela.
            transform.LookAt(target);
        }
    }
}