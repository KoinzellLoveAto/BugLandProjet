using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTrigger : MonoBehaviour
{

    public UnityEvent<ACharacter> OnCharacterEnterOnPlatform;
    public UnityEvent<ACharacter> OnCharacterLeavePlatform;

    private List<ACharacter> m_characterOnPlatform = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if it's a character
        ACharacter character = other.GetComponent<ACharacter>();

        if (character != null)
        {
            m_characterOnPlatform.Add(character);
            OnCharacterEnterOnPlatform?.Invoke(character);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Check if it's a character
        ACharacter character = other.GetComponent<ACharacter>();

        if (character != null)
        {
            OnCharacterLeavePlatform?.Invoke(character);
            if (m_characterOnPlatform.Contains(character))
                m_characterOnPlatform.Remove(character);
        }
    }
}
