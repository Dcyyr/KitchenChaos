using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{

    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField]
    private ContainerCounter m_ContainerCounter;

    private Animator m_Anime;
    private void Awake()
    {
        m_Anime = GetComponent<Animator>();
    }

    private void Start()
    {
        m_ContainerCounter.OnPlayerGrabbedObject += EOnPlayerGrabbedObjectFuc;
    }

    private void EOnPlayerGrabbedObjectFuc(object sender, System.EventArgs e)
    {
        m_Anime.SetTrigger(OPEN_CLOSE);

    }
}
