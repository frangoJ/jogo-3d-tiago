using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class GeradorPlayer : MonoBehaviour
{
    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private Transform spawnPointP1;
    [SerializeField] private Transform spawnPointP2;

    private void Awake()
    {
        if (inputManager == null)
            inputManager = GetComponent<PlayerInputManager>();
    }

    private void Start()
    {
        InstanciarJogadores();
        AtualizarTotalDeEstrelasNoGameManager();
    }

    public void InstanciarJogadores()
    {
        if (inputManager == null || inputManager.playerPrefab == null) return;

        PlayerInput p1 = inputManager.JoinPlayer(
            playerIndex: 0,
            splitScreenIndex: -1,
            controlScheme: "Keyboard P1",
            pairWithDevice: Keyboard.current
        );

        PlayerInput p2 = inputManager.JoinPlayer(
            playerIndex: 1,
            splitScreenIndex: -1,
            controlScheme: "Keybord P2",
            pairWithDevice: Keyboard.current
        );

        ConfigurarJogador(p1, 0, spawnPointP1, OutputChannels.Channel01);
        ConfigurarJogador(p2, 1, spawnPointP2, OutputChannels.Channel02);
    }

    private void ConfigurarJogador(PlayerInput player, int index, Transform spawn, OutputChannels channel)
    {
        if (player == null) return;

        if (spawn != null)
        {
            player.transform.position = spawn.position;
            player.transform.rotation = spawn.rotation;
        }

        PlayerMoedaCollector collector = player.GetComponent<PlayerMoedaCollector>();
        if (collector != null)
        {
            collector.playerIndex = index;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AllocatePlayerInput(player);
        }

        CinemachineCamera vcam = player.GetComponentInChildren<CinemachineCamera>();
        if (vcam != null)
        {
            vcam.OutputChannel = channel;
        }

        CinemachineBrain brain = player.GetComponentInChildren<CinemachineBrain>();
        if (brain != null)
        {
            brain.ChannelMask = channel;
        }
    }

    private void AtualizarTotalDeEstrelasNoGameManager()
    {
        if (GameManager.Instance != null)
        {
            // Busca todas as estrelas/pickups na cena e define a quantidade exata
            Pickup[] estrelas = FindObjectsByType<Pickup>(FindObjectsSortMode.None);
            GameManager.Instance.totalEstrelasNaCena = estrelas.Length;
            Debug.Log($"Total de estrelas encontradas na cena: {estrelas.Length}");
        }
    }
}