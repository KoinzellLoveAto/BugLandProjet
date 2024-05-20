using RakaEngine.Controllers.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField]
    private HearthImg hearthImg;

    [SerializeField]
    private HorizontalLayoutGroup m_layoutGroup;


    private List<HearthImg> m_hearthList = new List<HearthImg>();

    /// <summary>
    /// called by event (healthcontroller)
    /// </summary>
    /// <param name="a_healthcontroller"></param>
    public void Initialize(HealthController a_healthcontroller)
    {
        m_hearthList.Clear();
        DeleteAllChildren();


        for (int i = 0; i < a_healthcontroller.MaxHealthPoint; i++)
        {
            HearthImg img = Instantiate(hearthImg, m_layoutGroup.transform);
            if (i <= a_healthcontroller.StartHealth)
                img.SetHearth(HearthImg.EHearthType.full);
            else
                img.SetHearth(HearthImg.EHearthType.empty);

            m_hearthList.Add(img);
        }
    }

    private void DeleteAllChildren()
    {
        int childCount = m_layoutGroup.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(m_layoutGroup.transform.GetChild(i).gameObject);
        }
    }


    /// <summary>
    /// called by event (healthcontroller)
    /// </summary>
    /// <param name="a_healthController"></param>
    public void UpdateHealthBar(HealthController a_healthController)
    {
        for (int i = 0; i < m_hearthList.Count; i++)
        {
            if (i < a_healthController.currentHealth)
                m_hearthList[i].SetHearth(HearthImg.EHearthType.full);
            else
                m_hearthList[i].SetHearth(HearthImg.EHearthType.empty);
        }
    }

}
