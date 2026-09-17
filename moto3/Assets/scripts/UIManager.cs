using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Painel de Vitória")]
    public GameObject winPanel;
    public TextMeshProUGUI winText;

    private void Awake()
    {
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    private void Start()
    {
        RegistrarNoGameManager();
    }

    private void OnEnable()
    {
        RegistrarNoGameManager();
    }

    private void RegistrarNoGameManager()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarUI(this);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegistrarUI(null);
        }
    }
}