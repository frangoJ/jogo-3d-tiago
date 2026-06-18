using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            
            ChangeState(GameState.Iniciando);
            SceneManager.LoadScene("Splash"); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"[GameManager] estado mudou para: {CurrentState}");
    }

    public void RequestSceneChange(string sceneName)
    {
        if (CurrentState == GameState.Iniciando && sceneName == "Menu")
        {
            ChangeState(GameState.MenuPrincipal);
            SceneManager.LoadScene(sceneName);
        }
        else if (CurrentState == GameState.MenuPrincipal && sceneName == "Jogo")
        {
            ChangeState(GameState.Gameplay);
            SceneManager.LoadScene(sceneName);
            SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogWarning($"[GameManager] Bloqueado: Não posso ir para '{sceneName}' enquanto estou em {CurrentState}");
        }
    }

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