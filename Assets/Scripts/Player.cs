using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField]
    private float m_MoveSpeed = 5f;

    private bool m_IsWalking;
    private Vector3 m_LastInteractDir;
    private ClearCounter m_SelectedCounter;
    [SerializeField]
    private GameInput m_Input;
    [SerializeField]
    private LayerMask m_InteractLayerMask;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_Input.E_OnInteractAction += M_Input_E_OnInteractAction;
    }

    private void M_Input_E_OnInteractAction(object sender, System.EventArgs e)
    {

        Vector2 inputVector = m_Input.GetMovementNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        //防止没有方向键输出就不会检测的碰撞，（明明碰到了）
        if (moveDir != Vector3.zero)
        {
            m_LastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, m_LastInteractDir, out RaycastHit raycastHit, interactDistance, m_InteractLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
            else
            {
                Debug.Log("No clearCounter");
            }
        }
    }

    private void Update()
    {
        HandleInteraction();
        HandleMovement();
    }

    public bool IsWalking()
    {
        return m_IsWalking;
    }

    private void HandleMovement()
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


        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        m_IsWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }


    private void HandleInteraction()
    {

        Vector2 inputVector = m_Input.GetMovementNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        //防止没有方向键输出就不会检测的碰撞，（明明碰到了）
        if (moveDir != Vector3.zero)
        {
            m_LastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, m_LastInteractDir, out RaycastHit raycastHit, interactDistance, m_InteractLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != m_SelectedCounter)
                    SetSelectedCounter(clearCounter);

            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }


    }

    private void SetSelectedCounter(ClearCounter clearCounter)
    { 
        this.m_SelectedCounter = clearCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = m_SelectedCounter });

    }

}
