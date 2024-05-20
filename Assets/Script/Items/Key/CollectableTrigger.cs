using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class CollectableTrigger : MonoBehaviour, ICollectable
{

    public UnityEvent<PlayerCharacter> OnKeyCollected;

    public void Collect(PlayerCharacter a_character)
    {
        OnKeyCollected?.Invoke(a_character);

    }
}
