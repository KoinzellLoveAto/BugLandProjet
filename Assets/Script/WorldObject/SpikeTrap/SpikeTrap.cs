using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [field: SerializeField]
    public float damage { get; private set; } = 1f;
}
