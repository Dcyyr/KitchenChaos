using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField]
    private PlateKitchenObject m_PlateKitchenObject;
    [SerializeField]
    private Transform m_IconImage;


    private void Awake()
    {
        m_IconImage.gameObject.SetActive(false);
    }
    private void Start()
    {
        m_PlateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == m_IconImage) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in m_PlateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(m_IconImage, transform);
            iconTransform.gameObject.SetActive(true);

            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);

        }
    }
}
