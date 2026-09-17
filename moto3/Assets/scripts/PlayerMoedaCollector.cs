using UnityEngine;
using StarterAssets;

public class PlayerMoedaCollector : MonoBehaviour
{
    public int playerIndex = 0; 

    [Header("Aumento de Velocidade (Moedas)")]
    [SerializeField] private float incrementoVelocidade = 0.5f;
    [SerializeField] private float velocidadeMaxima = 12.0f;

    private int moedaCount = 0;
    private ThirdPersonController controller;

    private void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
    }

    // Chamado quando o CharacterController bate em algo com Collider rígido
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        ProcessarColeta(hit.gameObject);
    }

    // Chamado se o item for um Trigger
    private void OnTriggerEnter(Collider other)
    {
        ProcessarColeta(other.gameObject);
    }

    private void ProcessarColeta(GameObject item)
    {
        // 1. COLETA DE MOEDA (Velocidade)
        if (item.CompareTag("Moeda"))
        {
            moedaCount++;
            AumentarVelocidade();
            PlayerObserverManager.NotifyMoedaCollected(playerIndex);
            Destroy(item);
        }
        // 2. COLETA DE ESTRELA (Vitória)
        else if (item.CompareTag("Estrela") || item.GetComponent<Pickup>() != null)
        {
            // Instancia partículas no Pickup se houver
            Pickup pickup = item.GetComponent<Pickup>();
            if (pickup != null && pickup.particleEffectPrefab != null)
            {
                Instantiate(pickup.particleEffectPrefab, item.transform.position, Quaternion.identity);
            }

            // Envia evento de estrela coletada
            PlayerObserverManager.NotifyEstrelaCollected(playerIndex);
            Destroy(item);
        }
    }

    private void AumentarVelocidade()
    {
        if (controller != null)
        {
            controller.MoveSpeed = Mathf.Min(controller.MoveSpeed + incrementoVelocidade, velocidadeMaxima);
            controller.SprintSpeed = Mathf.Min(controller.SprintSpeed + incrementoVelocidade, velocidadeMaxima * 1.5f);
        }
    }
}