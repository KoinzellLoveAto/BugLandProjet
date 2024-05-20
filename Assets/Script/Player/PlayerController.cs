using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    public PlayerCharacter linkedCharacter {  get; private set; }

    private ControlMap controlMap;


    private bool m_shouldExecuteUpdate = false; 

    private void Awake()
    {
    }

    public void InitalizeController(ControlMap a_controller)
    {
        controlMap = a_controller;
    }

    public void RegisterEvents()
    {
        controlMap.Player.Enable();
        controlMap.Player.Shoot.started += Handle_ShootStarted;
        controlMap.Player.Shoot.canceled += Handle_ShootCanceled;
        controlMap.Player.Jump.performed += Handle_JumpPerformed;
        m_shouldExecuteUpdate = true;
    }

    public void StopListeningInput()
    {
        if (controlMap != null) 
        {
            controlMap.Player.Shoot.started -= Handle_ShootStarted;
            controlMap.Player.Shoot.canceled -= Handle_ShootCanceled;
            controlMap.Player.Jump.performed -= Handle_JumpPerformed;
            controlMap.Player.Disable();
            m_shouldExecuteUpdate=false;
        }
    }




    private void Handle_ShootStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        linkedCharacter.shooterController.HandleShootStarted();
    }

    public void Update()
    {
        if (!m_shouldExecuteUpdate) return;


        float dir = controlMap.Player.Move.ReadValue<float>();
        linkedCharacter.movementComponent.SetMoveInput(new Vector2(dir,0));

        linkedCharacter.playerAnimator.AdjustAnimationWithDirectinoInput(dir);
        linkedCharacter.movementComponent.AdjustSpriteWithDirectionX();


        //linkedCharacter
    }

    private void Handle_JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Jump Performed");
        linkedCharacter.movementComponent.Jump(); 
    }

    private void Handle_ShootCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        linkedCharacter.shooterController.HandleShootCanceled();
    }
}
