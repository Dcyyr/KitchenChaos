using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO m_KitchenObject;

    private ClearCounter m_ClearCounter;
    public KitchenObjectSO GetKitchenObject()
    {
        return m_KitchenObject;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        //清除当前的物体
        if (this.m_ClearCounter != null)
        {
            m_ClearCounter.ClearKitchenObject();
        }


        //分配新的物体在台面上
        this.m_ClearCounter = clearCounter;

        if(m_ClearCounter.HasKitchenObject())
        {
            Debug.Log("已经有物体在台面上");
        }
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return m_ClearCounter;
    }
}
