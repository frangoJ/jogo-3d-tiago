using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Necessário para o novo Input System

public class GameManager : MonoBehaviour
{
    // Implementação do padrão Singleton
    public static GameManager Instance { get; private set; }

    // Enum com os estados do jogo
    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        // Garante que exista apenas uma instância do GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Impede que seja destruído ao trocar de cena
            
            // Define o estado inicial e carrega a Splash automaticamente
            ChangeState(GameState.Iniciando);
            SceneManager.LoadScene("Splash"); 
        }
        else
        {
            Destroy(gameObject); // Destrói duplicatas caso existam
        }
    }

    // Função para alterar o estado e logar no console conforme exigido
    private void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"[GameManager] Estado alterado para: {CurrentState}");
    }

    // A ÚNICA porta de entrada para trocar de cenas no jogo
   public void RequestSceneChange(string sceneName)
{
    // 1. Se estiver iniciando e pedir o Menu
    if (CurrentState == GameState.Iniciando && sceneName == "Menu")
    {
        ChangeState(GameState.MenuPrincipal);
        SceneManager.LoadScene(sceneName);
    }
    // 2. Se estiver no Menu e pedir o Jogo
    else if (CurrentState == GameState.MenuPrincipal && sceneName == "Jogo")
    {
        ChangeState(GameState.Gameplay);
        SceneManager.LoadScene(sceneName);
    }
    else
    {
        // Se cair aqui, é porque o nome da cena enviado não bate com o IF 
        // ou o estado atual não permite essa transição.
        Debug.LogWarning($"[GameManager] Bloqueado: Não posso ir para '{sceneName}' enquanto estou em {CurrentState}");
    }
}
    // Gerenciamento simples de Input System exigido na atividade
    public void AllocatePlayerInput(PlayerInput playerInput)
    {
        if (CurrentState == GameState.Gameplay)
        {
            playerInput.ActivateInput();
            Debug.Log("[GameManager] Input alocado ao jogador com sucesso.");
        }
        else
        {
            playerInput.DeactivateInput();
            Debug.LogWarning("[GameManager] Input bloqueado. O jogo não está no estado Gameplay.");
        }
    }
}