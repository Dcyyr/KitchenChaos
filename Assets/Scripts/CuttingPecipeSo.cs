using UnityEngine;

[CreateAssetMenu()]
public class CuttingPecipeSo : ScriptableObject
{
    //这两个必须是一个东西，西红柿和西红柿片
    public KitchenObjectSO m_Input;//原材料
    public KitchenObjectSO m_Output;//切完之后的材料


    public int m_CuttingProgressMax;//切的次数
}
