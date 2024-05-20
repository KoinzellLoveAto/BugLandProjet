using RakaEngine.Controllers.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : ACharacter
{
    [field: SerializeField]
    private Rigidbody2D rb;

    [field: SerializeField]
    public MovementComponent movementComponent { get; private set; }


    [field: SerializeField]
    public HealthController healthController { get; private set; }

    [field:SerializeField]
    public PlayerAnimator playerAnimator { get; private set; }

    [field: SerializeField]
    public ShooterController shooterController { get; private set; }

    [field: SerializeField]
    public InventoryController inventoryController { get; private set; }

    [field: SerializeField]
    public RespawnController respawnController { get; private set; }

    [SerializeField]
    private Collider2D m_collider;

    [SerializeField]
    private Collider2D m_trigger;

    private void Start()
    {
        rb ??= GetComponent<Rigidbody2D>();

        if (rb == null)
            Debug.Log($"No RigidBody2D found on {gameObject.name}");

        healthController.Initialize();
        inventoryController.Initialize();

    }

    public void HandleDeath()
    {
        m_collider.enabled = false;
        m_trigger.enabled = false;
    }
}
