using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileTrigger : MonoBehaviour
{
    [SerializeField]
    private Projectile projectile;

    [SerializeField]
    private Collider2D circleCollider;

    [field:SerializeField]
    public LayerMask layerToIgnore {  get; private set; } 



    public UnityEvent OnBulletTouchSomething;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ignore layers selectionned
        if (layerToIgnore == (layerToIgnore | (1 << collision.gameObject.layer)))
            return;

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if(damageable != null)
        {
            damageable.Damage(projectile.projectileData.damage);
        }

        circleCollider.enabled = false;
        print(collision.gameObject);
        OnBulletTouchSomething?.Invoke();

    }
}
