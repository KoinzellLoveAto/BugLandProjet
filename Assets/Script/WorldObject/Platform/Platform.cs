using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [field: SerializeField]
    public GameObject objetToMove { get; private set; }


    [field: SerializeField]
    public List<Transform> pointList = new List<Transform>();

    [field: SerializeField]
    private float m_speed = 2;

    [field: SerializeField]
    private float m_timeToWaitBeforeChangeTarget;

    private Transform m_fromTarget => pointList[m_currentIndex];
    private Transform m_toTarget => GetNextTarget();


    private Coroutine m_waitCoroutine;

    private int m_currentIndex = 0;
    private bool m_hasReachedPosition = false;
    private bool m_shouldWait = false;
    private bool m_canMove = true;

    private void Start()
    {
        objetToMove.transform.position = pointList[0].position;
    }

    private void FixedUpdate()
    {
        if (HasReachedPosition() && !m_shouldWait)
        {
            m_canMove = false;  
            m_hasReachedPosition = true;
            m_shouldWait = true;
            m_waitCoroutine = StartCoroutine(Wait());
        }
        else if(m_canMove)
        {
            m_hasReachedPosition = false;
            MoveToNextPosition();
        }
    }

    private void MoveToNextPosition()
    {
        Vector3 dirPlatformToTarget = (m_toTarget.position - objetToMove.transform.position).normalized;
        objetToMove.transform.position += dirPlatformToTarget * m_speed * Time.fixedDeltaTime;
    }

    private bool HasReachedPosition()
    {
        Vector3 dirPlatformToTarget = (m_toTarget.position - objetToMove.transform.position).normalized;
        //check by dotproduct if direction platform is the "same" with dir point to point+1
        if (Vector3.Dot(dirPlatformToTarget, GetDirectionToTarget()) > 0)
            return true;
        else
            return false;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(m_timeToWaitBeforeChangeTarget);
        m_hasReachedPosition = false;

        if (m_currentIndex + 1 >= pointList.Count)
            m_currentIndex = 0;
        else
            m_currentIndex++;

        m_shouldWait = false;
        m_hasReachedPosition = false;
        m_canMove = true;


    }

    //called by event
    public void HandleCharacterEnterTrigger(ACharacter a_character)
    {
        a_character.transform.SetParent(objetToMove.transform);
    }

    public void HandleCharacterLeaveTrigger(ACharacter a_character)
    {
        a_character.transform.SetParent(null);
    }

    private Transform GetNextTarget()
    {
        if (m_currentIndex + 1 >= pointList.Count)
            return pointList[0];
        else
            return pointList[m_currentIndex + 1];
    }

    public Vector3 GetDirectionToTarget()
    {
        return (m_toTarget.position - m_fromTarget.position).normalized;
    }
}
