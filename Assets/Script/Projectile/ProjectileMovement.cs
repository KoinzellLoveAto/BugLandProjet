using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementComponent;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField]
    private Transform m_parentRoot;

    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    private Vector2 m_currentDirection;

    private ProjectileData m_ProjectileDataref;

    private bool m_freezeMovement = false;


    [SerializeField]
    private bool m_shouldReverseFlip = false;
    public void Initialize(ProjectileData a_data, Vector2 a_dir)
    {
        m_ProjectileDataref = a_data;
        m_currentDirection = a_dir;

        AdjustSpriteWithDirectionX();
    }

    private void FixedUpdate()
    {
        if (!m_freezeMovement)
            m_parentRoot.transform.position +=
                new Vector3(m_currentDirection.x, m_currentDirection.y, 0) * m_ProjectileDataref.speed * Time.fixedDeltaTime;
    }

    public void SetDirection(Vector2 a_dir)
    {
        m_currentDirection = a_dir;
    }

    public void AdjustSpriteWithDirectionX()
    {
        if (Mathf.Abs(m_currentDirection.x) > 0)
        {
            if (m_shouldReverseFlip == false)
            {
                if ((int)m_currentDirection.x < 0)
                {
                    m_SpriteRenderer.flipX = true;
                }
                else
                {
                    m_SpriteRenderer.flipX = false;
                }
            }
            else
            {
                if ((int)m_currentDirection.x < 0)
                {
                    m_SpriteRenderer.flipX = false;
                }
                else
                {
                    m_SpriteRenderer.flipX = true;
                }
            }
        }
    }

    public void StopMovement()
    {
        m_freezeMovement = true;
    }
}
