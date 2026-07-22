using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField]
    private PlatesCounter m_PlateCounter;

    [SerializeField]
    private Transform m_CounterTopPoint;
    [SerializeField]
    private Transform m_PlateVisualPrefab;


    private List<GameObject> m_PlateList;

    private void Awake()
    {
        m_PlateList = new List<GameObject>();
    }
    private void Start()
    {
        m_PlateCounter.OnPlateSpawned += PlateSpawned;
        m_PlateCounter.OnPlateRemoved += PlateRemoved;
    }

    private void PlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisual = Instantiate(m_PlateVisualPrefab, m_CounterTopPoint);

        // ”æı¿›∏ﬂ
        float plateYOffset = .1f;

        plateVisual.localPosition = new Vector3(0, plateYOffset * m_PlateList.Count, 0);

        m_PlateList.Add(plateVisual.gameObject);
    }

    private void PlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateObject = m_PlateList[m_PlateList.Count - 1];
        m_PlateList.Remove(plateObject);
        Destroy(plateObject);
    }
}
