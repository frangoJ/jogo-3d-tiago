using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    private int coinCount;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Encostou em: " + other.name);

        if (other.CompareTag("Moeda"))
        {
            coinCount++;

            PlayerObserverManager.NotifyCoinCollected(coinCount);

            Destroy(other.gameObject);
        }
    }
}