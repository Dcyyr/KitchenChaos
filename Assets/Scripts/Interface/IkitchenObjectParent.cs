using UnityEngine;

public interface IkitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();
      
    public void SetKitchenObject(KitchenObject obj);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();
   
}
