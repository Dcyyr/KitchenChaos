using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField]
    private KitchenObjectSO m_CutKitchenObject;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {    //上面没有物体，玩家手上有物体
            if (player.HasKitchenObject())
            {   //放到台子上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(m_CutKitchenObject, this);
        }

    }
}
