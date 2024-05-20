using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenuBtn : MonoBehaviour
{
    public void HandleToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
