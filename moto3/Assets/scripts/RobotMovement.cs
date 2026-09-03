using UnityEngine;
using UnityEngine.InputSystem; // Não esqueça desse usando!

public class RobotMovement : MonoBehaviour
{
    private Vector2 moveInput;

    // Essa função vai aparecer na lista do Unity Event!
    public void OnMove(InputAction.CallbackContext context)
    {
        // Lê o valor da alavanca/WASD/Setas
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Usa o moveInput para movimentar o robô
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);
        // Seu código de movimentação aqui...
    }
}