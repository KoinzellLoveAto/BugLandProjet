using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class DetectionTrigger : MonoBehaviour
{

    public UnityEvent<PlayerCharacter> OnTriggerStartPlayerCharacter;
    public UnityEvent<PlayerCharacter> OnTriggerEndPlayerCharacter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            OnTriggerStartPlayerCharacter?.Invoke((PlayerCharacter)damageable.GetOwnerCharacter());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            OnTriggerEndPlayerCharacter?.Invoke((PlayerCharacter)damageable.GetOwnerCharacter());
        }
    }
}
