using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField]
    private Image m_Icon;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        m_Icon.sprite = kitchenObjectSO.m_Sprite;
    }

}
