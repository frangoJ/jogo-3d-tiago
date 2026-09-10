using UnityEngine;
using TMPro; // Usado para TextMeshPro (remova se usar Text normal)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Pontuação")]
    public int scoreP1 = 0;
    public int scoreP2 = 0;

    [Header("UI do Placar")]
    [SerializeField] private TextMeshProUGUI textoPontuacaoP1;
    [SerializeField] private TextMeshProUGUI textoPontuacaoP2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método chamado pelo PlayerScore ao pegar a moeda
    public void AdicionarPontuacao(int playerIndex, int novaPontuacao)
    {
        if (playerIndex == 0)
        {
            scoreP1 = novaPontuacao;
            AtualizarTextoUI(textoPontuacaoP1, "P1 Moedas: ", scoreP1);
        }
        else if (playerIndex == 1)
        {
            scoreP2 = novaPontuacao;
            AtualizarTextoUI(textoPontuacaoP2, "P2 Moedas: ", scoreP2);
        }
    }

    private void AtualizarTextoUI(TextMeshProUGUI elementoTexto, string prefixo, int valor)
    {
        if (elementoTexto != null)
        {
            elementoTexto.text = prefixo + valor.ToString();
        }
    }

    public void AllocatePlayerInput(UnityEngine.InputSystem.PlayerInput player)
    {
        // Sua lógica de alocação de inputs mantida aqui
    }
}