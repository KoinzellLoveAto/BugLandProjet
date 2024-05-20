using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ShooterController : MonoBehaviour
{
    [SerializeField]
    Projectile prefab;

    [SerializeField]
    private float m_durationForChargeShoot = 1.5f;

    [SerializeField]
    private float m_delayBetweenShot = .2f;

    [SerializeField]
    private float m_delayToSpawnProjectileAfterShoot = .1f;

    [SerializeField]
    private Transform m_From;

    [SerializeField]
    private Vector2 m_offsetTransformOrigin;
        
    

    public UnityEvent OnPrepareToShoot;
    public UnityEvent OnShoot;
    public UnityEvent OnChargedShoot;


    private Vector2 m_directionShoot = new Vector2();
    private Coroutine m_chargedShotRoutine;
    private Coroutine m_shotDelayRoutine;
    private Coroutine m_delayedShootSpawnProjectileRoutine;

    private bool isChargedShot = false;
    private bool m_canShoot = true;
    private bool m_shouldConsumeShoot = false;

    public void SetDirectionShoot(Vector2 a_dirShoot)
    {
        m_directionShoot = a_dirShoot;
    }

    public void SetDirectionShoot(EDirectionLook a_dirLook)
    {
        if (a_dirLook == EDirectionLook.E_left)
        {
            m_directionShoot = Vector2.left;
        }
        else
        {
            m_directionShoot = Vector2.right;
        }

    }

    /// <summary>
    /// called when dammagedq
    /// </summary>
    public void CancelShoot()
    {
        if (m_chargedShotRoutine != null)
            StopCoroutine(m_chargedShotRoutine);
        m_shouldConsumeShoot = false;

        if (m_delayedShootSpawnProjectileRoutine != null)
            StopCoroutine(m_delayedShootSpawnProjectileRoutine);
    }

    public void HandleShootStarted()
    {
        if (!m_canShoot) return;

        m_shouldConsumeShoot = true;
        m_chargedShotRoutine = StartCoroutine(ChargedShootRoutine());
    }

    public void HandleShootCanceled()
    {
        TryToConsumeInput();
    }

    public bool TryToConsumeInput()
    {
        if (m_chargedShotRoutine != null)
            StopCoroutine(m_chargedShotRoutine);

        if (!m_canShoot) return false;


        if (isChargedShot)
        {
            //long shoot
            print("long shoot");
        }
        else
        {
            // simple shoot
            print("simple shoot");
        }
        isChargedShot = false;

        if (m_shouldConsumeShoot)
        {
            m_delayedShootSpawnProjectileRoutine = StartCoroutine(DelayedShootSpawnProjectile());
            
            return true;
        }
        return false;
    }

    public IEnumerator ChargedShootRoutine()
    {
        OnPrepareToShoot?.Invoke();
        yield return new WaitForSeconds(m_durationForChargeShoot);
        OnChargedShoot?.Invoke();
        isChargedShot = true;

    }

    public bool ShootRequest()
    {
        if (!m_canShoot) return false;

        m_shouldConsumeShoot = true;
        return TryToConsumeInput();
    }
    public IEnumerator ShotDelayRoutine()
    {
        m_canShoot = false;
        m_shouldConsumeShoot = false;

        yield return new WaitForSeconds(m_delayBetweenShot);
        m_canShoot = true;

    }

    private IEnumerator DelayedShootSpawnProjectile()
    {
        OnShoot?.Invoke();
        m_shouldConsumeShoot = false;
        m_shotDelayRoutine = StartCoroutine(ShotDelayRoutine());
        yield return new WaitForSeconds(m_delayToSpawnProjectileAfterShoot);
        SpawnProjectile();
    }

    private void SpawnProjectile()
    { 
        Vector2 offSetWithY = new Vector2(m_From.position.x,m_From.position.y + m_offsetTransformOrigin.y);
        Projectile bullet = Instantiate(prefab, offSetWithY + m_directionShoot*  m_offsetTransformOrigin.x , Quaternion.identity);
        bullet.Initialize(m_directionShoot);
    }

}
