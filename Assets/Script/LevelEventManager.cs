using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventManager : MonoBehaviour
{
    [field: SerializeField]
    public Transform SpawnLevelPoint;

    [field: SerializeField]
    public int m_nextLevelIndex;

    public static Action<Transform> OnLevelStarted;

    public static Action<int> OnLevelWin;


    void Start()
    {
        OnLevelStarted?.Invoke(SpawnLevelPoint);
    }

    public void HandleLevelWin()
    {
        OnLevelWin?.Invoke(m_nextLevelIndex);
    }
}
