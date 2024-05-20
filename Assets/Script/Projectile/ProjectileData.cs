using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/BulletData")]
public class ProjectileData : ScriptableObject
{
    [field: SerializeField]
    public float damage { get; private set; }

    [field: SerializeField]
    public float speed { get; private set; }

    [field: SerializeField]
    public float lifeTime { get; private set; }

}
