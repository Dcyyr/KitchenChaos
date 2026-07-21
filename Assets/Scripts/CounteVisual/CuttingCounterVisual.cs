using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{

    private const string CUT = "Cut";

    private Animator m_Anime;
    [SerializeField]
    private CuttingCounter m_CuttingCounter;
    private void Awake()
    {
        m_Anime = GetComponent<Animator>();

    }

    private void Start()
    {
        m_CuttingCounter.OnCut += CuttingCounterOnProgressChanged;

    }

 

    private void CuttingCounterOnProgressChanged(object sender, System.EventArgs e)
    {
        m_Anime.SetTrigger(CUT);
    }

  
}
