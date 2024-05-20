using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_textKey;

    private int MaxKey=3;

    public void UpdateKeyCounter(int a_keyCounter)
    {
        m_textKey.text = $"{ a_keyCounter.ToString()}/{MaxKey}"; 
    }

}
