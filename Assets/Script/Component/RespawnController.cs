using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [SerializeField]
    private Transform m_playerOrigine;

    [SerializeField]
    private float m_calculBetweenSecond = 2f;

    [SerializeField]
    private float m_distanceCheck = 1;

    [SerializeField]
    private LayerMask groundLayerMask;

    private Coroutine m_validPositionCheckerRoutine = null;
    private Vector2 m_lastValidePosition;


    private void Start()
    {
        m_validPositionCheckerRoutine = StartCoroutine(ValidPositionCheckerRoutine());
        m_lastValidePosition = m_playerOrigine.transform.position;
    }


    private IEnumerator ValidPositionCheckerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_calculBetweenSecond);
            TryToGetValidPosition();
        }
    }

    private bool TryToGetValidPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(m_playerOrigine.position, Vector2.down, m_distanceCheck, groundLayerMask);

        if (hit.transform == null) return false;

        if (hit.transform.CompareTag("Ground"))
        {
            m_lastValidePosition = hit.point;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RespawnToTheLastValidPosition()
    {
        Vector3 offSet = m_lastValidePosition;
        offSet.y += 1;
        m_playerOrigine.transform.position = offSet;
    }
}


