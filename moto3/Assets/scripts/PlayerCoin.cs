using UnityEngine;

public class PlayerCoin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();

        if (player != null)
        {
            player.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }
}