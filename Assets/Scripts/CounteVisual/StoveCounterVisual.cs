using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{


    [SerializeField]
    private GameObject m_Particle;
    [SerializeField]
    private GameObject m_StoveOnGameObject;
    [SerializeField]
    private StoveCounter m_StoveCounter;
    private void Awake()
    {

    }

    private void Start()
    {
        m_StoveCounter.OnStateChanged += StoveOnStateChanged;

    }



    private void StoveOnStateChanged(object sender, StoveCounter.EventOnStateChangedArgs e)
    {
        bool showVisual = e.m_State == StoveCounter.State.Frying || e.m_State == StoveCounter.State.Fried;
        m_StoveOnGameObject.SetActive(showVisual);
        m_Particle.SetActive(showVisual);
    }
}
