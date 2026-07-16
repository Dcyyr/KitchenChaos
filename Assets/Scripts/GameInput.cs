using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions m_PlayerInput;

    private void Awake()
    {
        m_PlayerInput = new InputSystem_Actions();
        m_PlayerInput.Player.Enable();
    }
    public Vector2 GetMovementNormalize()
    {

        Vector2 inputDir = m_PlayerInput.Player.Move.ReadValue<Vector2>();

        inputDir = inputDir.normalized;


        return inputDir;
    }
}
