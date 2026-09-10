using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Pontuação")]
    public int p1Score = 0;
    public int p2Score = 0;
    public int totalMoedasNaCena = 10;
    public int moedasColetadasTotal = 0;

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

    // -------------------------------------------------------------
    // REGISTRO DA INTERFACE DA CENA GUI
    // -------------------------------------------------------------
    public void RegistrarUI(UIManager ui)
    {
        uiManager = ui;
        AtualizarUI();
    }

    // -------------------------------------------------------------
    // GERENCIAMENTO DE MUDANÇA DE CENA (Assíncrono)
    // -------------------------------------------------------------
    public void RequestSceneChange(string nomeDaCena)
    {
        StartCoroutine(CarregarCenasProcesso(nomeDaCena));
    }

    private IEnumerator CarregarCenasProcesso(string nomeDaCena)
    {
        // 1. Reseta o placar ao trocar de cena
        p1Score = 0;
        p2Score = 0;
        moedasColetadasTotal = 0;
        uiManager = null; // Limpa referência antiga da UI

        // 2. Carrega a cena solicitada (Ex: "Jogo", "Menu", "Splash")
        AsyncOperation opGameplay = SceneManager.LoadSceneAsync(nomeDaCena, LoadSceneMode.Single);
        while (!opGameplay.isDone)
        {
            yield return null;
        }

        // 3. Carrega a cena GUI aditivamente APENAS se a cena carregada for a de gameplay ("Jogo")
        if (nomeDaCena == "Jogo")
        {
            AsyncOperation opGUI = SceneManager.LoadSceneAsync("GUI", LoadSceneMode.Additive);
            while (!opGUI.isDone)
            {
                yield return null;
            }
        }
    }

    // -------------------------------------------------------------
    // LÓGICA DE PONTUAÇÃO E PLACAR
    // -------------------------------------------------------------
    public void AdicionarPontuacao(int playerIndex)
    {
        moedasColetadasTotal++;

        if (playerIndex == 0)
        {
            p1Score++;
        }
        else if (playerIndex == 1)
        {
            p2Score++;
        }

        AtualizarUI();

        if (moedasColetadasTotal >= totalMoedasNaCena)
        {
            ExibirTelaDeVitoria();
        }
    }

    public void AtualizarUI()
    {
        if (uiManager == null) return;

        if (uiManager.p1ScoreText != null)
            uiManager.p1ScoreText.text = $"PLAYER 1: {p1Score}";

        if (uiManager.p2ScoreText != null)
            uiManager.p2ScoreText.text = $"PLAYER 2: {p2Score}";

        if (uiManager.totalRemainingText != null)
            uiManager.totalRemainingText.text = $"RESTANTES: {totalMoedasNaCena - moedasColetadasTotal}";
    }

    private void ExibirTelaDeVitoria()
    {
        if (uiManager == null) return;

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

    public void AllocatePlayerInput(PlayerInput player)
    {
        if (player == null) return;
        player.SwitchCurrentActionMap("Player");
    }
}