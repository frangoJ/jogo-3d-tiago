using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Pontuação de Estrelas")]
    public int p1Score = 0;
    public int p2Score = 0;
    public int totalEstrelasNaCena = 10;
    public int estrelasColetadasTotal = 0;

    private UIManager uiManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PlayerObserverManager.OnEstrelaCollected += OnEstrelaColetadaRecebida;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnEstrelaCollected -= OnEstrelaColetadaRecebida;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "_Boot")
        {
            RequestSceneChange("Splash");
        }
    }

    private void OnEstrelaColetadaRecebida(int playerIndex)
    {
        AdicionarPontuacaoEstrela(playerIndex);
    }

    public void RegistrarUI(UIManager ui)
    {
        uiManager = ui;

        if (uiManager != null)
        {
            if (uiManager.winPanel != null)
                uiManager.winPanel.SetActive(false);
        }
    }

    public void RequestSceneChange(string nomeDaCena)
    {
        StartCoroutine(CarregarCenasProcesso(nomeDaCena));
    }

    private IEnumerator CarregarCenasProcesso(string nomeDaCena)
    {
        p1Score = 0;
        p2Score = 0;
        estrelasColetadasTotal = 0;
        uiManager = null;

        AsyncOperation opGameplay = SceneManager.LoadSceneAsync(nomeDaCena, LoadSceneMode.Single);
        while (!opGameplay.isDone)
        {
            yield return null;
        }

        if (nomeDaCena == "Jogo")
        {
            AsyncOperation opGUI = SceneManager.LoadSceneAsync("GUI", LoadSceneMode.Additive);
            while (!opGUI.isDone)
            {
                yield return null;
            }
        }
    }

    public void AdicionarPontuacaoEstrela(int playerIndex)
    {
        estrelasColetadasTotal++;

        if (playerIndex == 0)
        {
            p1Score++;
        }
        else if (playerIndex == 1)
        {
            p2Score++;
        }

        Debug.Log($"Estrela coletada por P{playerIndex + 1}! Total: {estrelasColetadasTotal}/{totalEstrelasNaCena}");

        if (estrelasColetadasTotal >= totalEstrelasNaCena)
        {
            Debug.Log("Vitória atingida! Exibindo tela de vitória...");
            ExibirTelaDeVitoria();
        }
    }

    private void ExibirTelaDeVitoria()
    {
        // Fallback caso o UIManager não tenha sido atribuído no registro
        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }

        if (uiManager != null)
        {
            if (uiManager.winPanel != null)
                uiManager.winPanel.SetActive(true);

            if (uiManager.winText != null)
            {
                if (p1Score > p2Score)
                    uiManager.winText.text = "PLAYER 1 VENCEU!";
                else if (p2Score > p1Score)
                    uiManager.winText.text = "PLAYER 2 VENCEU!";
                else
                    uiManager.winText.text = "EMPATE!";
            }
        }
        else
        {
            Debug.LogError("ERRO: UIManager não foi encontrado na cena!");
        }
    }

    public void AllocatePlayerInput(PlayerInput player)
    {
        if (player == null) return;
        player.SwitchCurrentActionMap("Player");
    }
}