using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerLevel : MonoBehaviour
{
    [SerializeField]
    public int nbKeyToHave = 3;

    public UnityEvent OnLevelEnded;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerTrigger trigger = collision.GetComponent<PlayerTrigger>();

        if(trigger != null )
        {
           PlayerCharacter character =  (PlayerCharacter)trigger.GetOwnerCharacter(); 
            
            if(character.inventoryController.KeyCounter >= nbKeyToHave)
            {
                OnLevelEnded?.Invoke();
            }
        }
    }
}
