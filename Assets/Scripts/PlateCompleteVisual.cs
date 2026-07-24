using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObject
    {
        public KitchenObjectSO m_KitchenObjectSO;
        public GameObject m_GameObject;
    }


    [SerializeField]
    private PlateKitchenObject m_PlateKitchenObject;
    [SerializeField]
    private List<KitchenObject> m_KitchenObjectOnPlateList;

    private void Start()
    {
        m_PlateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnIngredientAdded;
    }

    private void PlateKitchenObjectOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(var kitchenObject in m_KitchenObjectOnPlateList)
        {
            if (kitchenObject.m_KitchenObjectSO == e.m_KitchenObjectSO)
                kitchenObject.m_GameObject.SetActive(true);
        }

    }
}
