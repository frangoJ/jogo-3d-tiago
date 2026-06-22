using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinCollected += UpdateCoinText;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinCollected -= UpdateCoinText;
    }

    private void Start()
    {
        if (coinText != null)
        {
            coinText.text = "Moedas: 0";
        }
    }

    private void UpdateCoinText(int totalCoins)
    {
        if (coinText != null)
        {
            coinText.text = "Moedas: " + totalCoins;
        }
    }
}