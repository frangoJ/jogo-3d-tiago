using System;

public static class PlayerObserverManager
{
    public static event Action<int> OnMoedaCollected;

    public static void NotifyMoedaCollected(int valor)
    {
        OnMoedaCollected?.Invoke(valor);
    }
}