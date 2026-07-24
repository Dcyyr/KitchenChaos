using System.Collections.Generic;
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
       {    //玩家拿着一些东西
            if(player.HasKitchenObject())
            {   //玩家拿着盘子
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {   //拿着盘子去装可以装的东西
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    { 
                        GetKitchenObject().DestroySelf();

                    }

                }else
                {   //玩家没有拿着除盘子以外的东西
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {   //玩家把东西放到盘子上
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
       }
       

    }


}
