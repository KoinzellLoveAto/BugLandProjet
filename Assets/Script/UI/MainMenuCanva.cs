using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanva : MonoBehaviour
{
    [field: SerializeField]
    public GameObject PausePanel { get; private set; }

    [field: SerializeField]
    public GameObject GameOverPanel { get; private set; }



    public void ShowGameOverPanel(bool a_value)
    {
        GameOverPanel.SetActive(a_value);
    }


    public void ShowGameOverPanel(bool a_value, float a_time)
    {
        StartCoroutine(DelayedShowGameOverPanel(a_value,a_time));
    }
   
    private IEnumerator DelayedShowGameOverPanel(bool a_value, float a_time)
    {
        yield return new WaitForSeconds(a_time);
        ShowGameOverPanel(a_value);
    }


    public void ShowPausePanel(bool a_value)
    {
        PausePanel.SetActive(a_value);
    }
}
