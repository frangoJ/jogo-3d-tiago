using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    private int coinCount;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Coin") || other.CompareTag("PlayerCoin"))
        {
            coinCount++;
            PlayerObserverManager.NotifyCoinCollected(coinCount);
            Destroy(other.gameObject);
        }
    }
}