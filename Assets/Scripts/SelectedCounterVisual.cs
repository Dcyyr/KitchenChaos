using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField]
    private BaseCounter m_BaseCounter;
    [SerializeField]
    private GameObject[] m_VisualGameObject;


    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
    }

    private void PlayerOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == m_BaseCounter)
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
        foreach (var visualGameObject in m_VisualGameObject)
        {
            visualGameObject.SetActive(true);

        }
    }
    private void Hide()
    {
        foreach (var visualGameObject in m_VisualGameObject)
        {
            visualGameObject.SetActive(false);

        }

    }

}
