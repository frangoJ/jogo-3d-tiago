using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI coinText;

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
        coinText.text = "Moedas: 0";
    }

    private void UpdateCoinText(int totalCoins)
    {
        coinText.text = "Moedas: " + totalCoins;
    }
}