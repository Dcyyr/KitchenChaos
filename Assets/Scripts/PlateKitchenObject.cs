using System;
using System.Collections.Generic;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO m_KitchenObjectSO;
    }



    private List<KitchenObjectSO> m_KitchenObjectSO;

    public List<KitchenObjectSO> m_VaildObject;

    private void Awake()
    {
        m_KitchenObjectSO = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {

        if (!m_VaildObject.Contains(kitchenObjectSO))
        {
            return false;
        }

        if (m_KitchenObjectSO.Contains(kitchenObjectSO))
        {
            return false;

        }
        else
        {
            m_KitchenObjectSO.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{

                m_KitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return m_KitchenObjectSO;
    }

}
