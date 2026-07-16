using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float m_MoveSpeed = 5f;

    private bool m_IsWalking;
    [SerializeField]
    private GameInput m_Input;
    private void Update()
    {
        Vector2 inputVector = m_Input.GetMovementNormalize();

        Vector3 dir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += dir * m_MoveSpeed * Time.deltaTime;

        m_IsWalking = dir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,dir,Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return m_IsWalking;
    }

}
