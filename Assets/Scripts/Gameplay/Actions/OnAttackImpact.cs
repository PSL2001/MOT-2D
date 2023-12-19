using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackImpact : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    [SerializeField] bool destroyOnImpact = true;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f);   
    }

    public void Initialize(float newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player") )
        {
            collision.gameObject.GetComponentInParent<ActorController>()?.OnDamage(damage);
            if (destroyOnImpact) { Destroy(gameObject); }
        }
        else if (collision.CompareTag("Floor")) {
            if (destroyOnImpact) { Destroy(gameObject); }
        }
    }
}
