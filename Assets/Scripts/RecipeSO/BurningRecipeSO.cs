using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO m_Input;
    public KitchenObjectSO m_Output;

    public float m_MaxBurningTime;
}
