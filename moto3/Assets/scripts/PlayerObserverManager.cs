using System;

public static class PlayerObserverManager
{
    // O evento agora fala Moeda
    public static event Action<int> OnMoedaCollected;

    public static void NotifyMoedaCollected(int totalMoedas)
    {
        OnMoedaCollected?.Invoke(totalMoedas);
    }
}