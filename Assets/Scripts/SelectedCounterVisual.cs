using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField]
    private ClearCounter m_ClearCounter;
    [SerializeField]
    private GameObject m_VisualGameObject;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
    }

    private void PlayerOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == m_ClearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        m_VisualGameObject.SetActive(true);
    }

    private void Hide()
    {
        m_VisualGameObject.SetActive(false);

    }

}
