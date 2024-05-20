using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HearthImg : MonoBehaviour
{
    public enum EHearthType
    {
        full,
        half,
        empty
    }

    [SerializeField]
    private Image m_img;

    [SerializeField]
    private Sprite m_HearthFull;

    [SerializeField]
    private Sprite m_HearthHalf;

    [SerializeField]
    private Sprite m_HearthEmpty;


    public void SetHearth(EHearthType a_HearthType)
    {
        switch (a_HearthType)
        {
            case EHearthType.full:
                m_img.sprite = m_HearthFull;
                break;

            case EHearthType.half:
                m_img.sprite = m_HearthHalf;
                break;
            case EHearthType.empty:
                m_img.sprite = m_HearthEmpty;
                break;
        }
    }

}
