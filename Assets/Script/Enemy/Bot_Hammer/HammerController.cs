using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    [SerializeField]
    private HammerCharacter m_linkedCharacter;


    private PlayerCharacter m_targetCharacter;

    [Header("AI data comportement")]
    [SerializeField]
    private float m_triggerAttackRange = 2f;

    private bool m_shouldExecuteLogic = true;

    public void OnPlayerStartTriggered(PlayerCharacter a_character)
    {
        m_targetCharacter = a_character;
    }

    public void OnPlayerEndTriggered(PlayerCharacter a_character)
    {

        m_targetCharacter = null;
    }

    private void Update()
    {
        if (!m_shouldExecuteLogic) return;

        if (m_targetCharacter != null)
        {
            m_linkedCharacter.movementController.SetMoveInput(GetDirXWithPlayerTarget());
            m_linkedCharacter.movementController.AdjustSpriteWithDirectionX();
        
            
            if (GetDistanceWithPlayerTarget() < m_triggerAttackRange)
            {
                m_linkedCharacter.shooterController.SetDirectionShoot(GetDirXWithPlayerTarget());
                m_linkedCharacter.shooterController.ShootRequest();
                m_linkedCharacter.movementController.StopMovement(true);

            }

        }
        

    }

    public void HandleDeath()
    {
        m_shouldExecuteLogic = false;
        m_linkedCharacter.movementController.StopMovement(true);
    }

    private Vector2 GetDirXWithPlayerTarget()
    {
        if (m_targetCharacter != null)
        {
            if (m_linkedCharacter.transform.position.x < m_targetCharacter.transform.position.x)
            {
                return Vector2.right;
            }
            else
                return Vector2.left;
            
        }
        return Vector2.zero;
    }

    private float GetDistanceWithPlayerTarget()
    {
        return Vector2.Distance(m_linkedCharacter.transform.position, m_targetCharacter.transform.position);
    }



}
