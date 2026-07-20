using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter
{

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float m_ProgressNormalized;
    }

    public event EventHandler OnCut;



    [SerializeField]
    private CuttingPecipeSo[] m_CutKitchenObject;

    private int m_CuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {    //上面没有物体，玩家手上有物体
            if (player.HasKitchenObject())
            {
                //如果玩家手上的物体可以切，就放到台子上
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //放到台子上切
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    m_CuttingProgress = 0;

                    CuttingPecipeSo cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        m_ProgressNormalized = (float)m_CuttingProgress / cuttingRecipeSO.m_CuttingProgressMax
                    });
                }
             
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

    //切东西互动F键
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            m_CuttingProgress++;
            //放到台子上不会播放动画，只有切东西才会播放动画
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingPecipeSo cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
            {
                m_ProgressNormalized = (float)m_CuttingProgress / cuttingRecipeSO.m_CuttingProgressMax
            });

            //切好后变成片
            if (m_CuttingProgress >= cuttingRecipeSO.m_CuttingProgressMax)
            {
                //获取对应的KitchenObjectSO
                KitchenObjectSO OutputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(OutputKitchenObject, this);
            }
         
        }

    }

    //检查这个东西是否可以切
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObject)
    {
        foreach(CuttingPecipeSo cutting in m_CutKitchenObject)
        {
            if (cutting.m_Input == inputKitchenObject)
                return true;
        }

        return false;
    }

    //切对应的KitchenObject，如果是西红柿就切成西红柿片，如果是生菜就切成生菜片
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputObject)
    {
        foreach (CuttingPecipeSo cuttingObj in m_CutKitchenObject)
        {
            if(cuttingObj.m_Input == inputObject)
            {
                return cuttingObj.m_Output;
            }
        }

        return null;
    }

    private  CuttingPecipeSo GetCuttingRecipeSOWithInput(KitchenObjectSO inputObject)
    {
        foreach (CuttingPecipeSo cuttingObj in m_CutKitchenObject)
        {
            if (cuttingObj.m_Input == inputObject)
            {
                return cuttingObj;
            }
        }
        return null;
    }
}
