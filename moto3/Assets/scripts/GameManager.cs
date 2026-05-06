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
        // Lógica de autorização baseada no estado atual e cena solicitada
        if (CurrentState == GameState.Iniciando && sceneName == "Menu Principal")
        {
            ChangeState(GameState.MenuPrincipal);
            SceneManager.LoadScene(sceneName);
        }
        else if (CurrentState == GameState.MenuPrincipal && sceneName == "GetStarted_Scene")
        {
            ChangeState(GameState.Gameplay);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            // Caso algum script tente carregar uma cena de forma não autorizada
            Debug.LogWarning($"[GameManager] Mudança para a cena '{sceneName}' NÃO autorizada a partir do estado {CurrentState}.");
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