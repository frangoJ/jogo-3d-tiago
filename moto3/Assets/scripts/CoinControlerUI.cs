using UnityEngine;
using TMPro;

public class CoinControlerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        PlayerObserverManager.OnCoinsChanged += UpdateCoinText;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnCoinsChanged -= UpdateCoinText;
    }

    private void UpdateCoinText(int currentCoins)
    {
        if (coinText != null)
        {
            coinText.text = $"Moedas: {currentCoins}";
        }
    }
}