using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO m_Input;
    public KitchenObjectSO m_Output;

    public float m_MaxFryingTime;
}
