using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private void OnEnable()
    {
        // Se inscreve usando o nome atualizado
        PlayerObserverManager.OnMoedaCollected += UpdateCoinText;
    }

    private void OnDisable()
    {
        // Desinscreve usando o nome atualizado
        PlayerObserverManager.OnMoedaCollected -= UpdateCoinText;
    }

    private void Start()
    {
        if (coinText != null)
        {
            coinText.text = "Moedas: 0";
        }
    }

    private void UpdateCoinText(int totalMoedas)
    {
        if (coinText != null)
        {
            coinText.text = "Moedas: " + totalMoedas;
        }
    }
}