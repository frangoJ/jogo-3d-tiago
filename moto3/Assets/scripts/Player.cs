using UnityEngine;

public class PlayerMoedaCollector : MonoBehaviour
{
    private int moedaCount = 0;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Moeda"))
        {
            moedaCount++;
            
            // Chama o método atualizado em português!
            PlayerObserverManager.NotifyMoedaCollected(moedaCount);
            
            Destroy(hit.gameObject);
        }
    }
}