using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class RobotMovement : MonoBehaviour
{
    private StarterAssetsInputs starterInputs;

    private void Awake()
    {
        starterInputs = GetComponent<StarterAssetsInputs>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (starterInputs != null)
        {
            starterInputs.MoveInput(context.ReadValue<Vector2>());
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (starterInputs != null)
        {
            starterInputs.JumpInput(context.ReadValueAsButton());
        }
    }
}