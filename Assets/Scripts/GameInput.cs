using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputSystem_Actions m_PlayerInput;
    public event EventHandler E_OnInteractAction;
    private void Awake()
    {
        m_PlayerInput = new InputSystem_Actions();
        m_PlayerInput.Player.Enable();

        m_PlayerInput.Player.Interaction.performed += Interact_performed;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        E_OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementNormalize()
    {

        Vector2 inputDir = m_PlayerInput.Player.Move.ReadValue<Vector2>();

        inputDir = inputDir.normalized;


        return inputDir;
    }
}
