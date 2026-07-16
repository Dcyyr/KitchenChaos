using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string WALKING = "Walking";

    private Player m_Player;
    private Animator m_Anim;

    private void Awake()
    {
        m_Player = GetComponent<Player>();
        m_Anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        m_Anim.SetBool(WALKING, m_Player.IsWalking());

    }
}
