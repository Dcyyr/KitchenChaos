using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO m_KitchenObjectPrefab;
    [SerializeField]
    private Transform m_CounterTopPoint;

    private KitchenObject m_KitchenObject;

    [SerializeField] private ClearCounter m_SecondCounter;
    [SerializeField]
    private bool m_Test;
    private void Update()
    {
        if(m_Test && Input.GetKeyDown(KeyCode.Q))
        {
            if(m_KitchenObject !=null)
            {
                m_KitchenObject.SetClearCounter(m_SecondCounter);
            }
        }
    }
    public void Interact()
    {
        if (m_KitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(m_KitchenObjectPrefab.m_Prefab, m_CounterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
            //让这个物体不为0，就不会一直重复生成了
        }
        else
        {
            Debug.Log(m_KitchenObject.GetClearCounter());
        }
       

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return m_CounterTopPoint;
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
