using RakaEngine.Controllers.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCharacter : ACharacter
{
    [field:SerializeField]
    public  HammerAnimatorController animatorController{ get; private set; }

    [field: SerializeField]
    public HealthController healthController { get; private set; }

    [field: SerializeField]
    public MovementComponent movementController { get; private set; }

    [field: SerializeField]
    public ShooterController shooterController { get; private set; }

    [field:SerializeField]
    public Collider2D collider { get; private set; }

    [field: SerializeField]
    public Collider2D trigger { get; private set; }

    private void Start()
    {
        healthController.Initialize();

    }

    public void HandleDeath()
    {
        collider.enabled = false;
        trigger.enabled = false;
    }

    public IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
