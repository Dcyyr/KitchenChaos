using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour,IkitchenObjectParent
{

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField]
    private float m_MoveSpeed = 5f;
    [SerializeField]
    private GameInput m_Input;
    [SerializeField]
    private LayerMask m_InteractLayerMask;
    [SerializeField]
    private Transform m_kitchenObjectHoldPoint;

    private KitchenObject m_KitchenObject;
    private bool m_IsWalking;
    private Vector3 m_LastInteractDir;
    private BaseCounter m_SelectedCounter;



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
        m_Input.E_OnInteractAction += InteractAction;
    }

    private void InteractAction(object sender, System.EventArgs e)
    {

        if(m_SelectedCounter !=null)
            m_SelectedCounter.Interact(this);
       
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
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != m_SelectedCounter)
                    SetSelectedCounter(baseCounter);

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

    private void SetSelectedCounter(BaseCounter clearCounter)
    { 
        this.m_SelectedCounter = clearCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = m_SelectedCounter });

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return m_kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject obj)
    {
        this.m_KitchenObject = obj;
    }

    public KitchenObject GetKitchenObject()
    {
        return m_KitchenObject;
    }

    public void ClearKitchenObject()
    {
        m_KitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return m_KitchenObject != null;
    }
}
