using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionOnChild2d : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponentInParent<ActorController>().GetGameObject().transform;
            player.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponentInParent<ActorController>().GetGameObject().transform;
            player.SetParent(null);
        }
    }
}
