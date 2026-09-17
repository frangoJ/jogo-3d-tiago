using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    [Header("Effects")]
    public GameObject particleEffectPrefab;

    [Header("Motion Settings")]
    public float rotationSpeed = 100f;
    public float bobbingAmount = 0.1f;
    public float bobbingSpeed = 1f;

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        timer += Time.deltaTime * bobbingSpeed;
        float newY = startPosition.y + Mathf.Sin(timer) * bobbingAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        int playerIndex = -1;

        // 1. Tenta identificar o playerIndex via PlayerMoedaCollector presente no Robô
        PlayerMoedaCollector collector = other.GetComponent<PlayerMoedaCollector>();
        if (collector != null)
        {
            playerIndex = collector.playerIndex;
        }
        // 2. Se não achar no collector, tenta buscar pelo PlayerInput do New Input System
        else if (other.GetComponent<PlayerInput>() != null)
        {
            playerIndex = other.GetComponent<PlayerInput>().playerIndex;
        }

        // Se encontrou um jogador válido
        if (playerIndex != -1)
        {
            if (particleEffectPrefab != null)
            {
                Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            }

            // Notifica o evento global que uma Estrela foi coletada
            PlayerObserverManager.NotifyEstrelaCollected(playerIndex);

            Destroy(gameObject);
        }
    }
}