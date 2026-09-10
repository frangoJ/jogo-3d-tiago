using System.Collections;
using UnityEngine;

public class BootManager : MonoBehaviour
{
    [SerializeField] private string proximaCena = "Menu";
    [SerializeField] private float tempoEspera = 0.5f;

    private IEnumerator Start()
    {
        // 1. Aguarda um frame para garantir que o GameManager.Awake() já executou
        yield return null;

        // 2. Espera um tempo opcional (útil se tiver splash/logo)
        if (tempoEspera > 0)
        {
            yield return new WaitForSeconds(tempoEspera);
        }

        // 3. Chama a troca de cena através do GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RequestSceneChange(proximaCena);
        }
        else
        {
            Debug.LogError("GameManager não encontrado na cena _Boot!");
        }
    }
}