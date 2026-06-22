using System;

public static class PlayerObserverManager
{
    public static Action<int> OnCoinCollected;

    public static void NotifyCoinCollected(int totalCoins)
    {
        OnCoinCollected?.Invoke(totalCoins);
    }
}