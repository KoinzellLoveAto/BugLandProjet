using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBtn : MonoBehaviour
{
    public void Continue()
    {
        GameManager.ManagePause();
    }
}
