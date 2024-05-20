using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : MonoBehaviour
{
    [SerializeField]
    private int m_coinAmount;


    /// <summary>
    /// called by event (CollectableTrigger)
    /// </summary>
    /// <param name="a_player"></param>
    public void OnItemCollected(PlayerCharacter a_player)
    {
        a_player.inventoryController.AddCoin(m_coinAmount);
        AutoDestroy();
    }


    private void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
