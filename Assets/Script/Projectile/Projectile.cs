using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [field:SerializeField]
    public ProjectileAnimator projectileAnimator {  get; private set; }

    [field: SerializeField]
    public ProjectileMovement projectileMovement { get; private set; }

    [field:SerializeField]
    public ProjectileData projectileData { get; private set; }

    [field: SerializeField] 
    private Collider2D m_collider;

    private Coroutine m_lifeTimeRoutine;


    public UnityEvent OnReachedLifeTime;


    public void Initialize(Vector2 a_direction)
    {
        projectileMovement.Initialize(projectileData, a_direction);
        AutoDestroyWithLifeTime();
    }

    public void HandleEndFade()
    {
        Destroy(this.gameObject);
    }
    
    public void AutoDestroyWithLifeTime()
    {
        m_lifeTimeRoutine = StartCoroutine(LifeTimeRoutine());
    }

    public void CancelAutoDestroyRoutine()
    {
        StopCoroutine(m_lifeTimeRoutine);
    }

    private IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(projectileData.lifeTime);
        m_collider.enabled = false;
        OnReachedLifeTime?.Invoke();

    }
}
