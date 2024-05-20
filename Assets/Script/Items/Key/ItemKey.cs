using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKey : MonoBehaviour
{
    [field: SerializeField]
    public int NbKey { get; private set; } = 1;

    /// <summary>
    /// called by CollectableTrigger
    /// </summary>
    /// <param name="a_player"></param>
    public void OnKeyCollected(PlayerCharacter a_player)
    {
        a_player.inventoryController.AddKey(NbKey);
        AutoDestroy();
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }


    

}
