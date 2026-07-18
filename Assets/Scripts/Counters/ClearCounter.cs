using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectSO m_KitchenObjectPrefab;
 
    
    public override void Interact(Player player)
    {
       if(!HasKitchenObject())
        {    //上面没有物体，玩家手上有物体
            if (player.HasKitchenObject())
            {   //放到台子上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
       }
       else
       {
            if(player.HasKitchenObject())
            {
                
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
       }
       

    }


}
