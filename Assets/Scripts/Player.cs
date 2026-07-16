using UnityEngine;
using UnityEngine.EventSystems;

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

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerHeight = 2f;
        float playerRadius = 0.5f;
        float moveDistance = m_MoveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //贴着墙斜向键盘输出时同时按下两个方向键，尝试沿着X轴或Z轴移动
        if (!canMove)
        {
            //尝试X移动
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //尝试Z移动
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }


        if(canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        m_IsWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir,Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return m_IsWalking;
    }

}
