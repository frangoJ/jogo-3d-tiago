using UnityEngine;

public class Player : MonoBehaviour
{
    private int totalCoins = 0;

    private void Start()
    {
        PlayerObserverManager.SendCoinsChanged(totalCoins);
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        PlayerObserverManager.SendCoinsChanged(totalCoins);
    }
}