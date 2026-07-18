using UnityEngine;

public class BaseCounter : MonoBehaviour,IkitchenObjectParent
{

   
    [SerializeField]
    private Transform m_CounterTopPoint;

    protected KitchenObject m_KitchenObject;

    public virtual void Interact(Player player)
    {

    }

    public virtual void InteractAlternate(Player player)
    {

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
