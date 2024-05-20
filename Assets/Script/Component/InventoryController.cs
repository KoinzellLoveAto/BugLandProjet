using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryController : MonoBehaviour
{

    private int m_coinCounter;
    private int m_keyCounter;

    public UnityEvent<int> OnCoinCountChange;
    public UnityEvent<int> OnKeyCountChange;

    public int CoinCounter
    {
        get
        {
            return m_coinCounter;
        }
        private set
        {
            m_coinCounter = value;
            OnCoinCountChange?.Invoke(m_coinCounter);

        }
    }

    public int KeyCounter
    {
        get
        {
            return m_keyCounter;
        }
        private set
        {
            m_keyCounter = value;
            OnKeyCountChange?.Invoke(m_keyCounter);

        }
    }

    public void Initialize()
    {
        KeyCounter = 0;
        CoinCounter = 0;    
    }


    public void AddKey(int a_amount)
    {
        KeyCounter = KeyCounter +a_amount;
    }

    public void AddCoin(int a_amount)
    {
        CoinCounter = CoinCounter + a_amount;
    }
}
