using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(float a_value, bool a_enviromentalDamage = false);
    public ACharacter GetOwnerCharacter();
    
}
