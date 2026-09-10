using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // O GeradorPlayer vai definir este valor (0 = P1, 1 = P2)
    public int playerIndex = 0;

    private int score = 0;

    // Referência do script de movimento para aumentar velocidade
    private StarterAssets.ThirdPersonController controller;

    private void Awake()
    {
        controller = GetComponent<StarterAssets.ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score++;

            // 1. Aumenta a velocidade do robô a cada moeda coletada
            if (controller != null)
            {
                controller.MoveSpeed += 1.0f;
            }

            // 2. Avisa o GameManager enviando O ÍNDICE DESTE PLAYER
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AdicionarPontuacao(playerIndex, score);
            }

            // Destrói a moeda
            Destroy(other.gameObject);
        }
    }
}