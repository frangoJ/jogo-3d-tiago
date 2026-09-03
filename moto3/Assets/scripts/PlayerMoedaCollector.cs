using UnityEngine;

public class PlayerMoedaCollector : MonoBehaviour
{
    private int moedaCount = 0;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Teste de Log: imprime tudo em que o robô esbarra
        Debug.Log("Robô encostou no objeto: " + hit.gameObject.name + " | Tag: " + hit.gameObject.tag);

        if (hit.gameObject.CompareTag("Moeda"))
        {
            moedaCount++;

            PlayerObserverManager.NotifyMoedaCollected(moedaCount);

            Destroy(hit.gameObject);
        }
    }
}