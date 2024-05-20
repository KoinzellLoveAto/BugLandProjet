using RakaEngine.Controllers.Health;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour, IDamageable
{
    [SerializeField]
    private PlayerCharacter character;


    public void Damage(float a_value, bool a_enviromentalDamage = false)
    {
        character.healthController.Dammage(a_value);
        if (a_enviromentalDamage && character.healthController.currentHealth > character.healthController.MinHealthPoint)
            character.respawnController.RespawnToTheLastValidPosition();
    }

    public ACharacter GetOwnerCharacter()
    {
        return character;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICollectable collectable = collision.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Collect(character);
        }

    }
}
