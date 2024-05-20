using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;


public enum EDirectionLook
{
    E_left,
    E_right
}

public class MovementComponent : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Transform transformRoot;

    [SerializeField]
    private SpriteRenderer m_SpriteRenderer;

    [field: Header("speed Movement")]
    [SerializeField]
    private float m_speedStrength;

    [SerializeField]
    private float m_jumpStrength;

    [field: SerializeField]
    public LayerMask layerCanJump { get; private set; }



    [field: Header("Variable check ground")]
    [field: SerializeField]
    private float distanceToBeGrounded = 0.2f;

    private bool isGrounded;
    private Vector2 moveInput;

    public UnityEvent<EDirectionLook> OnDirectionChange;

    public EDirectionLook currentDirection { get; private set; }

    private bool m_freezeMovementUpdate = false;


    public void SetMoveInput(Vector2 a_moveInput)
    {
        moveInput = a_moveInput;
    }


    public void StopMovement(bool a_value)
    {
        if (a_value)
        {
            SetMoveInput(Vector2.zero);
            m_freezeMovementUpdate = true;
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            m_freezeMovementUpdate = false;
            rb.gravityScale = 1;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }


    private void Start()
    {
        currentDirection = EDirectionLook.E_left;
        OnDirectionChange?.Invoke(currentDirection);
        m_freezeMovementUpdate = false;
    }

    private void Update()
    {
        isGrounded = IsGrounded();
    }

    public void AdjustSpriteWithDirectionX()
    {
        if (Mathf.Abs(moveInput.x) > 0)
        {
            if ((int)moveInput.x < 0)
            {
                if (currentDirection == EDirectionLook.E_right)
                {
                    currentDirection = EDirectionLook.E_left;
                    OnDirectionChange?.Invoke(currentDirection);
                }

                m_SpriteRenderer.flipX = true;
            }
            else
            {
                if (currentDirection == EDirectionLook.E_left)
                {
                    currentDirection = EDirectionLook.E_right;
                    OnDirectionChange?.Invoke(currentDirection);
                }

                m_SpriteRenderer.flipX = false;
            }
        }
    }


    public void FixedUpdate()
    {
        if (m_freezeMovementUpdate) return;

        Vector2 movement = new Vector2(moveInput.x * m_speedStrength * Time.fixedDeltaTime, 0);

        transformRoot.position = (Vector2)transformRoot.position + movement;

    }



    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * m_jumpStrength);
        }
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.Raycast(transformRoot.position, Vector2.down, distanceToBeGrounded, layerCanJump);
        if (grounded)
        {
            Debug.DrawLine
                (transformRoot.position, transformRoot.position + Vector3.down * distanceToBeGrounded, Color.red);
        }
        else
        {
            Debug.DrawLine
                (transformRoot.position, transformRoot.position + Vector3.down * distanceToBeGrounded, Color.green);
        }
        return grounded;
    }

}
