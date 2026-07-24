using System;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<EventOnStateChangedArgs> OnStateChanged;

    public class EventOnStateChangedArgs : EventArgs
    {
        public State m_State;
    }



    public enum State
    {
        None,
        Frying,
        Fried,
        Burned
    
    }

    [SerializeField]
    private FryingRecipeSO[] m_FryingRecipeSOArray;
    [SerializeField]
    private BurningRecipeSO[] m_BurningRecipeSOArray;

    private float m_FryingTimer;
    private FryingRecipeSO m_FryingSO;
    private float m_BurningTimer;
    private BurningRecipeSO m_BurningSO;

    private State m_CurrentState;

    private void Start()
    {
        m_CurrentState = State.None;
    }
    private void Update()
    {

        switch (m_CurrentState)
        {
            case State.None:
                break;
            case State.Frying:
                if (HasKitchenObject())
                {
                    m_FryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        m_ProgressNormalized = m_FryingTimer / m_FryingSO.m_MaxFryingTime

                    });
                    if (m_FryingTimer > m_FryingSO.m_MaxFryingTime)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(m_FryingSO.m_Output, this);

                        m_BurningSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        m_CurrentState = State.Fried;
                        m_BurningTimer = 0;

                        OnStateChanged?.Invoke(this, new EventOnStateChangedArgs
                        {
                            m_State = m_CurrentState

                        });

                      
                    }
                }
                break;
            case State.Fried:
                if (HasKitchenObject())
                {
                    m_BurningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        m_ProgressNormalized = m_BurningTimer / m_BurningSO.m_MaxBurningTime

                    });
                    if (m_BurningTimer > m_BurningSO.m_MaxBurningTime)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(m_BurningSO.m_Output, this);

                        m_CurrentState = State.Burned;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            m_ProgressNormalized = 0

                        });

                        OnStateChanged?.Invoke(this, new EventOnStateChangedArgs
                        {
                            m_State = m_CurrentState

                        });
                    }
                }
                break;
            case State.Burned:
                break;

        }


       
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {    //上面没有物体，玩家手上有物体
            if (player.HasKitchenObject())
            {
                //如果玩家手上的物体可以切，就放到台子上
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //放到台子上切
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    m_FryingSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    m_CurrentState = State.Frying;
                    m_FryingTimer = 0;

                    OnStateChanged?.Invoke(this, new EventOnStateChangedArgs
                    {
                        m_State = m_CurrentState

                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        m_ProgressNormalized = m_FryingTimer / m_FryingSO.m_MaxFryingTime

                    });

                }

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();

                        m_CurrentState = State.None;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            m_ProgressNormalized = 0

                        });

                        OnStateChanged?.Invoke(this, new EventOnStateChangedArgs
                        {
                            m_State = m_CurrentState

                        });

                    }

                }
            }
            else
            {
                //玩家从台子上拿起来对应物品
                GetKitchenObject().SetKitchenObjectParent(player);
                m_CurrentState = State.None;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    m_ProgressNormalized = 0

                });

                OnStateChanged?.Invoke(this, new EventOnStateChangedArgs
                {
                    m_State = m_CurrentState

                });
            }
        }
    }
    //检查这个东西是否可以切
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObject)
    {
        foreach (FryingRecipeSO FryingObj in m_FryingRecipeSOArray)
        {
            if (FryingObj.m_Input == inputKitchenObject)
                return true;
        }

        return false;
    }

    //切对应的KitchenObject，如果是西红柿就切成西红柿片，如果是生菜就切成生菜片
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputObject)
    {
        foreach (FryingRecipeSO FryingObj in m_FryingRecipeSOArray)
        {
            if (FryingObj.m_Input == inputObject)
            {
                return FryingObj.m_Output;
            }
        }

        return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputObject)
    {
        foreach (FryingRecipeSO FryingObj in m_FryingRecipeSOArray)
        {
            if (FryingObj.m_Input == inputObject)
            {
                return FryingObj;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputObject)
    {
        foreach (BurningRecipeSO BurningObj in m_BurningRecipeSOArray)
        {
            if (BurningObj.m_Input == inputObject)
            {
                return BurningObj;
            }
        }
        return null;
    }


}
