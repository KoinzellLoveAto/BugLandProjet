using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBotTrigger : MonoBehaviour, IDamageable
{

    [SerializeField]
    private HammerCharacter m_character;

    public void Damage(float a_value, bool a_enviromentalDamage)
    {
        m_character.healthController.Dammage(a_value);
    }

    public ACharacter GetOwnerCharacter()
    {
        return m_character;
    }
}
