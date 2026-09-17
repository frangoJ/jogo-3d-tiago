using System;

public static class PlayerObserverManager
{
    // Evento para MOEDAS (Aumentar velocidade e HUD de moedas)
    public static event Action<int> OnMoedaCollected;

    // Evento para ESTRELAS (Contagem de vitória no GameManager)
    public static event Action<int> OnEstrelaCollected;

    public static void NotifyMoedaCollected(int playerIndex)
    {
        OnMoedaCollected?.Invoke(playerIndex);
    }

    public static void NotifyEstrelaCollected(int playerIndex)
    {
        OnEstrelaCollected?.Invoke(playerIndex);
    }
}