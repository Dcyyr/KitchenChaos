using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField]
    private KitchenObjectSO m_KitchenObjectPrefab;
    
    public override void Interact(Player player)
    {
        //玩家手上没有东西
        if (!player.HasKitchenObject())
        {
           KitchenObject.SpawnKitchenObject(m_KitchenObjectPrefab, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            //让这个物体不为0，就不会一直重复生成了
        }
       
    }


}
