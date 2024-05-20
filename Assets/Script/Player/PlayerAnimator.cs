using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [field: SerializeField]
    private Animator m_Animator;

    [field: SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    int IsMoving = Animator.StringToHash("IsMoving");
    int PrepareShoot = Animator.StringToHash("PrepareShoot");
    int Shoot = Animator.StringToHash("Shoot");
    int Damaged = Animator.StringToHash("Damaged");
    int Death = Animator.StringToHash("Death");


    public void PlayIdle()
    {
        m_Animator.SetBool(IsMoving, false);
    }

    public void PlayMove()
    {
        m_Animator.SetBool(IsMoving, true);

    }

    public void PlayPrepareShoot()
    {
        m_Animator.SetTrigger(PrepareShoot);
    }

    public void PlayShoot()
    {
        m_Animator.SetTrigger(Shoot);

    }

    public void PlayerDamaged()
    {
        m_Animator.SetTrigger(Damaged);
    }

    public void PlayDeath()
    {
            m_Animator.SetTrigger(Death);
    }

    public void AdjustAnimationWithDirectinoInput(float a_dir)
    {

        //check if we dont touch input
        if ((int)a_dir == 0)
        {
            PlayIdle();
        }
        else
        {
            PlayMove();

        }

    }
}
