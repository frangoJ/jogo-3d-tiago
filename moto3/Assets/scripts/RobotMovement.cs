using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets; // Namespace do Starter Assets

public class RobotMovement : MonoBehaviour
{
    private StarterAssetsInputs starterInputs;

    private void Awake()
    {
        // Pega o componente do Starter Assets no próprio robô
        starterInputs = GetComponent<StarterAssetsInputs>();
    }

    // Função chamada pelo Unity Event do PlayerInput
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();

        // Repassa o movimento e a animação direto para o controlador nativo
        if (starterInputs != null)
        {
            starterInputs.MoveInput(moveVector);
        }
    }
}