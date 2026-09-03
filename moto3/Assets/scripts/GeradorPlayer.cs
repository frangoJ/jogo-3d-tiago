using UnityEngine;
using UnityEngine.InputSystem;

public class GeradorPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Arraste o Prefab do Robô no campo Player Prefab do GeradorPlayer!");
            return;
        }

        // Jogador 1 usando "Keyboard P1" (com espaço)
        PlayerInput p1 = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: 0,
            controlScheme: "Keyboard P1",
            pairWithDevice: Keyboard.current
        );

        // Jogador 2 usando "Keybord P2" (exatamente como está escrito no asset)
        PlayerInput p2 = PlayerInput.Instantiate(
            playerPrefab,
            playerIndex: 1,
            controlScheme: "Keybord P2",
            pairWithDevice: Keyboard.current
        );
    }
}