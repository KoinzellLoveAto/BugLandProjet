using RakaEngine.Controllers.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator m_AnimatorController;


    private bool isDead = false;

    int m_speed = Animator.StringToHash("Speed");
    int m_death = Animator.StringToHash("Death");
    int m_damaged = Animator.StringToHash("Damaged");
    int m_attack = Animator.StringToHash("Attack");

    public void PlayDamaged()
    {
        m_AnimatorController.SetTrigger(m_damaged);
    }

    public void PlayDeath()
    {
        if (!isDead)
        {
            isDead = true;
            m_AnimatorController.SetTrigger(m_death);
        }
    }

    public void PlayAttack()
    {
        m_AnimatorController.SetTrigger(m_attack);
    }

    public void PlaySpeed(float speed)
    {
        m_AnimatorController.SetFloat(m_speed, Mathf.Clamp01(speed));
    }
}
