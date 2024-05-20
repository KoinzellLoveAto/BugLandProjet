using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI TextMeshPro;

    public void UpdateCoinCount(int a_coinCounter)
    {
        TextMeshPro.text = a_coinCounter.ToString();
    }


}
