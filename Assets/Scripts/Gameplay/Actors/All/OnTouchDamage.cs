using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ActorController))]
public class OnTouchDamage : MonoBehaviour
{
    public UnityEvent onHit = new();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            float damage = GetComponent<ActorController>().GetStats().Damage;
            collision.gameObject.GetComponent<ActorController>().OnDamage(damage);
            onHit.Invoke();
        }
    }
}
