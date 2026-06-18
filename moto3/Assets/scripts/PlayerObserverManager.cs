using System;

public static class PlayerObserverManager
{
    public static event Action<int> OnCoinsChanged;

    public static void SendCoinsChanged(int currentCoins)
    {
        OnCoinsChanged?.Invoke(currentCoins);
    }
}