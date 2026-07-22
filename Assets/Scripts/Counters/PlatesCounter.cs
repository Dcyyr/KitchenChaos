using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField]
    private KitchenObjectSO m_PlateKitchenObjectSO;

    private float m_SpwanPlateTimer;
    private float m_SpwanPlateTimerMax = 2f;

    private int m_PlatesSpwanedAmount;
    private int m_PlatesSpwanedAmountMax = 4;
    private void Update()
    {
        m_SpwanPlateTimer += Time.deltaTime;

        if(m_SpwanPlateTimer >= m_SpwanPlateTimerMax)
        {
            m_SpwanPlateTimer = 0;

            if(m_PlatesSpwanedAmount < m_PlatesSpwanedAmountMax)
            {
                m_PlatesSpwanedAmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
        
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            if(m_PlatesSpwanedAmount >0)
            {
                m_PlatesSpwanedAmount--;
                KitchenObject.SpawnKitchenObject(m_PlateKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
