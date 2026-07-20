using UnityEngine;
using UnityEngine.UI;

public class ProgressUI : MonoBehaviour
{
    [SerializeField]
    private Image m_Image;
    [SerializeField]
    private CuttingCounter m_CuttingCounter;

    private void Start()
    {
        m_CuttingCounter.OnProgressChanged += CuttingCounterOnProgressChanged;

        m_Image.fillAmount = 0f;
        HideUI();
    }

    private void CuttingCounterOnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        m_Image.fillAmount = e.m_ProgressNormalized;

        //没切之前和切完了都隐藏UI，其他时候显示UI
        if (e.m_ProgressNormalized == 0 || e.m_ProgressNormalized == 1)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    private void ShowUI()
    {
        gameObject.SetActive(true);
    }

    private void HideUI()
    {
        gameObject.SetActive(false);

    }
    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
