using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField]
    private KitchenObjectSO m_KitchenObjectPrefab;
    
    public override void Interact(Player player)
    {
        if (m_KitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(m_KitchenObjectPrefab.m_Prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            //让这个物体不为0，就不会一直重复生成了
        }
       
    }


}
