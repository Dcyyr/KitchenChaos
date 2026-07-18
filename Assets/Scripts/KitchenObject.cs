using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO m_KitchenObject;

    private IkitchenObjectParent m_kitchenObjectParent;
    public KitchenObjectSO GetKitchenObject()
    {
        return m_KitchenObject;
    }

    public void SetKitchenObjectParent(IkitchenObjectParent kitchenObjParent)
    {
        //清除当前的物体
        if (this.m_kitchenObjectParent != null)
        {
            m_kitchenObjectParent.ClearKitchenObject();
        }


        //分配新的物体在台面上
        this.m_kitchenObjectParent = kitchenObjParent;

        if(m_kitchenObjectParent.HasKitchenObject())
        {
            Debug.Log("已经有物体在台面上");
        }
        kitchenObjParent.SetKitchenObject(this);

        transform.parent = kitchenObjParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IkitchenObjectParent GetKitchenObjectParent()
    {
        return m_kitchenObjectParent;
    }
}
